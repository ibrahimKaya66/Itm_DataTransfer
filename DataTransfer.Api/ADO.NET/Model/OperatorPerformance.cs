namespace DataTransfer.Api.ADO.NET.Model
{
    public class OperatorPerformance
    {
        public string Employee_Name { get; set; }
        public int EmployeeId { get; set; }
        public string Operation_Name { get; set; }
        public string Operation_Type { get; set; }
        public decimal TimeSecond { get; set; }
        public string Line_Name { get; set; }
        public string Group_Name { get; set; }
        public string GroupCode_Name { get; set; }
        public string Department_Name { get; set; }
        public decimal Performance { get; set; }
    }
}
