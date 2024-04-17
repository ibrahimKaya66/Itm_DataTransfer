using DataTransfer.Model.Ado.Net;
using System.Data.SqlClient;

namespace DataTransfer.Api.ADO.NET
{
    public static class Db
    {
        public static List<OperatorPerformance> OperatorPerformances(string? startDate = null, string? endDate = null)
        {
            var config = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appsettings.json")
             .Build();

            string? sqlConnectionString = config["AppSettings:DefaultConnection"];
            var models = new List<OperatorPerformance>();
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                try
                {
                    sqlConnection.Open();
                    string conditions = string.Empty;
                    if (string.IsNullOrEmpty(startDate) && string.IsNullOrEmpty(endDate))
                        conditions = $" WHERE CONVERT(date, WorkersDailyProcess.CreateDate) = 'CONVERT(date, {DateTime.Now.Date})'";
                    else if (!string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate))
                        conditions = $" WHERE CONVERT(date, WorkersDailyProcess.CreateDate) BETWEEN '{startDate}' AND '{endDate}' ";
                    else if (string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate))
                        conditions = $" WHERE CONVERT(date, WorkersDailyProcess.CreateDate) <= '{endDate}' ";
                    else if (!string.IsNullOrEmpty(startDate) && string.IsNullOrEmpty(endDate))
                        conditions = $" WHERE CONVERT(date, WorkersDailyProcess.CreateDate) >= '{startDate}'";

                    string cmdText = @$"
                                        SELECT 
                                        Employees.Employee_Name,
                                        Employee_DailyProduction.Employee_id,
                                        Job_Title.Job_title,
                                        Balance_Model_Operation.Operation_Name,
                                        (Balance_Model_Operation.OperationToplamSTD*60) as OperationSec,
                                        Worker_Line.Line_Name,
                                        Worker_Line.RaspberryKey,
                                        Worker_Line.Target_Performance,
                                        Groups.Group_Type,
                                        Groups.Group_Name,
                                        Department.Depart_Name,
                                        Machine.Machine_Type,   -- Makine tipi gösteriyor.  EL = HAND OLARAK DÖNÜYOR. MAKİNE = MACH OLARAK DÖNÜYOR 
                                        (SUM(WorkersDailyProcess.Real_Amount * Balance_Model_Operation.OperationToplamSTD)) / nullif((SUM(DATEDIFF(MINUTE, WorkersDailyProcess.Start_time, ISNULL(WorkersDailyProcess.End_time, CONVERT(time,WorkersDailyProcess.LastRead_Date))))),0) as Performans
                                        FROM     WokerLine_OrdersRouting WITH (nolock) INNER JOIN
                                                          Balance_Model WITH (nolock) ON WokerLine_OrdersRouting.Model_Balance_Id = Balance_Model.Id INNER JOIN
                                                          Balance_Model_Operation WITH (nolock) ON Balance_Model.Id = Balance_Model_Operation.Model_Balance_Id INNER JOIN
                                                          BalanceOperationCode_List WITH (nolock) ON Balance_Model_Operation.Id = BalanceOperationCode_List.BalanceModelOperation_Id INNER JOIN
                                                          WorkersDailyProcess WITH (nolock) ON BalanceOperationCode_List.Id = WorkersDailyProcess.BalanceOperationCode_Id INNER JOIN
                                                          Employee_DailyProduction ON WorkersDailyProcess.Employee_DailyProduction_Id = Employee_DailyProduction.Id INNER JOIN
                                                          Employees ON Employee_DailyProduction.Employee_id = Employees.Id  INNER JOIN
				                                          Worker_Line with (nolock) on WokerLine_OrdersRouting.Line_Id = Worker_Line.Id INNER JOIN
				                                          Groups on Balance_Model_Operation.Group_Id = Groups.Groups_id INNER JOIN
				                                          Department ON Balance_Model_Operation.Department_Id = Department.Id  INNER JOIN
				                                          Machine with (nolock) on Balance_Model_Operation.Machine_id = Machine.Machine_id INNER JOIN
				                                          Job_Title with (nolock) on Employees.Job_title_id = Job_title.Job_id
                                        {conditions} 
                                        GROUP BY  
                                        Employees.Employee_Name,
                                        Job_Title.Job_title,
                                        Employee_DailyProduction.Employee_id,
                                        Balance_Model_Operation.Operation_Name,
                                        Balance_Model_Operation.OperationToplamSTD,
                                        Worker_Line.Line_Name,
                                        Worker_Line.RaspberryKey,
                                        Worker_Line.Target_Performance,
                                        Groups.Group_Type,
                                        Department.Depart_Name,
                                        Groups.Group_Name,
                                        Machine.Machine_Type
                                        order by 
                                        Balance_Model_Operation.Operation_Name
                        ";

                    using (var sqlCommand = new SqlCommand(cmdText, sqlConnection))
                    {
                        SqlDataReader reader = sqlCommand.ExecuteReader();
                        while (reader.Read())
                        {
                            var model = new OperatorPerformance();
                            model.Employee_Name = reader[0]?.ToString() ?? "";
                            model.EmployeeId = reader.GetInt32(1);
                            model.Job_Name = reader[2]?.ToString() ?? "";
                            model.Operation_Name = reader[3]?.ToString() ?? "";
                            model.TimeSecond = reader.GetDecimal(4);
                            model.Line_Name = reader[5]?.ToString() ?? "";
                            model.LcdNo = int.Parse(reader[6]?.ToString() ?? "");
                            model.TargetProductivity = reader.GetDecimal(7);
                            model.GroupCode_Name = reader[8]?.ToString() ?? "";
                            model.Group_Name = reader[9]?.ToString() ?? "";
                            model.Department_Name = reader[10]?.ToString() ?? "";
                            model.Operation_Type = reader[11]?.ToString() ?? "";
                            model.Performance = reader.GetDecimal(12);
                            models.Add(model);
                        }
                        reader.Close();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Bağlantı hatası: " + ex.Message);
                }
            }
            return models;

        }
        public static List<StyleOperation> StyleOperations(string styleName)
        {
            var config = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appsettings.json")
             .Build();

            string? sqlConnectionString = config["AppSettings:DefaultConnection"];
            var models = new List<StyleOperation>();
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                try
                {
                    sqlConnection.Open();
                    string conditions = string.Empty;

                    conditions = $" WHERE Model.Model_Name = '{styleName}' ";

                    string cmdText = @$"
                                        select 
                                        Operation.Oper_Sequence,
                                        Model_Name,
                                        Model_Code,
                                        Groups.Group_Name as KatalogGroup,
                                        Operation.Operation_Name,
                                        (Operation.OperationToplamSTD*60) as TimeSec,
                                        Customers.Customer_Name,
                                        Machine.Machine_Type as OperationType ,
                                        Department.Depart_Name,
                                        OperationGroups.Group_Name as OperationGroupName
                                        from Operation
                                        inner join Model with (nolock) on Operation.Model_id = Model.Id
                                        inner join Groups with (nolock) on Model.Group_Id = Groups.Groups_id
                                        inner join Customers with (nolock) on Model.Customer_id = Customers.Customer_id
                                        inner join Machine with (nolock) on Operation.Machine_id = Machine.Machine_id
                                        inner join Department with(nolock) on Operation.Department_Id = Department.Id
                                        inner join Groups as OperationGroups with(nolock) on Operation.Operation_Groups = OperationGroups.Groups_id
                                        {conditions}
                                        order by 
                                        Operation.Oper_Sequence
                        ";

                    using (var sqlCommand = new SqlCommand(cmdText, sqlConnection))
                    {
                        SqlDataReader reader = sqlCommand.ExecuteReader();
                        while (reader.Read())
                        {
                            var model = new StyleOperation();
                            model.EntityOrder = reader.GetInt32(0);
                            model.StyleName = reader[1]?.ToString() ?? "";
                            model.StyleCode = reader[2]?.ToString() ?? "";
                            model.CatalogGroupName = reader[3]?.ToString() ?? "";
                            model.OperationName = reader[4]?.ToString() ?? "";
                            model.TimeSecond = reader.GetDecimal(5);
                            model.CustomerName = reader[6]?.ToString() ?? "";
                            model.OperationType = reader[7]?.ToString() ?? "";
                            model.DepartmentName = reader[8]?.ToString() ?? "";
                            model.OperationGroupName = reader[9]?.ToString() ?? "";
                            models.Add(model);
                        }
                        reader.Close();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Bağlantı hatası: " + ex.Message);
                }
            }
            return models;

        }
    }
}
