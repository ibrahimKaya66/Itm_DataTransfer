﻿using DataTransfer.Business.Methods.Abstract;
using DataTransfer.Business.Services.Abstract;
using DataTransfer.Model.Ado.Net;
using DataTransfer.Model.Entities;

namespace DataTransfer.Business.Methods.Concrete
{
    public class DataTransferMethod : IDataTransferMethod
    {
        private readonly IDepartmentService departmentService;
        private readonly IEmployeeService employeeService;
        private readonly IFactoryService factoryService;
        private readonly IGroupCodeService groupCodeService;
        private readonly IGroupService groupService;
        private readonly IJobService jobService;
        private readonly ILineService lineService;
        private readonly IOperationPerformanceService operationPerformanceService;
        private readonly IOperationService operationService;

        public DataTransferMethod(IDepartmentService departmentService, IEmployeeService employeeService, IFactoryService factoryService, IGroupCodeService groupCodeService, IGroupService groupService, IJobService jobService, ILineService lineService, IOperationPerformanceService operationPerformanceService, IOperationService operationService)
        {
            this.departmentService = departmentService;
            this.employeeService = employeeService;
            this.factoryService = factoryService;
            this.groupCodeService = groupCodeService;
            this.groupService = groupService;
            this.jobService = jobService;
            this.lineService = lineService;
            this.operationPerformanceService = operationPerformanceService;
            this.operationService = operationService;
        }

        public async Task DataTransfer(List<OperatorPerformance> model)
        {
            Department department = new Department();
            Employee employee = new Employee();
            GroupCode groupCode = new GroupCode();
            Group group = new Group();
            Job job = new Job();
            Line line = new Line();
            OperationPerformance operationPerformance = new OperationPerformance();
            Operation operation = new Operation();

            DateTime utcNow = DateTime.UtcNow;
            var factory = factoryService.GetAll().FirstOrDefault();
            var utc = Convert.ToDouble(factory?.Country.UtcOffset ?? 3);
            DateTime now = utcNow.AddHours(utc);

            foreach (var item in model)
            {
                //department add
                department = await departmentService.GetAsync(d => d.Name == item.Department_Name);
                if(department == null)
                {
                    department = new Department() 
                    {
                        Name = item.Department_Name,
                        FactoryId = factory?.Id ?? 1,
                        CreatedDate = now
                    };
                    await departmentService.AddAsync(department);
                    department = await departmentService.GetAsync(d => d.Name == item.Department_Name);//eklenenin Id bilgisini çek
                }

                //job add
                job = await jobService.GetAsync(j => j.Name == item.Job_Name);
                if (job == null)
                {
                    job = new Job()
                    {
                        Name = item.Job_Name,
                        CreatedDate = now
                    };
                    await jobService.AddAsync(job);
                    job = await jobService.GetAsync(j => j.Name == item.Job_Name);//eklenenin Id bilgisini çek
                }

                //Employee
                employee = await employeeService.GetAsync(e => e.FullName == item.Employee_Name);
                if (employee == null)
                {
                    employee = new Employee()
                    {
                        FullName = item.Employee_Name,
                        DepartmentId = department.Id,
                        JobId = job.Id,
                        ExpenseTypeId = 10,//direct 
                        SourceId = item.EmployeeId,
                        CreatedDate = now
                    };
                    await employeeService.AddAsync(employee);
                    employee = await employeeService.GetAsync(e => e.FullName == item.Employee_Name);
                }

                //groupCode add
                groupCode = await groupCodeService.GetAsync(g => g.Name == item.GroupCode_Name);
                if (groupCode == null)
                {
                    groupCode = new GroupCode()
                    {
                        Name = item.GroupCode_Name,
                        CreatedDate = now
                    };
                    await groupCodeService.AddAsync(groupCode);
                    groupCode = await groupCodeService.GetAsync(g => g.Name == item.GroupCode_Name);//eklenenin Id bilgisini çek
                }

                //group add
                group = await groupService.GetAsync(g => g.Name == item.Group_Name);
                if (group == null)
                {
                    group = new Group()
                    {
                        Name = item.Group_Name,
                        GroupCodeId = groupCode.Id,
                        CreatedDate = now
                    };
                    await groupService.AddAsync(group);
                    group = await groupService.GetAsync(g => g.Name == item.Group_Name);//eklenenin Id bilgisini çek
                }

                //line add
                line = await lineService.GetAsync(l => l.Name == item.Line_Name);
                if (line == null)
                {
                    line = new Line()
                    {
                        Name = item.Line_Name,
                        DepartmentId = department.Id,
                        TargetProductivity = item.TargetProductivity,
                        LCDNo = item.LcdNo,
                        CreatedDate = now
                    };
                    await lineService.AddAsync(line);
                    line = await lineService.GetAsync(l => l.Name == item.Line_Name);//eklenenin Id bilgisini çek
                }

                //operation add
                operation = await operationService.GetAsync(o => o.Name == item.Operation_Name);
                if (operation == null)
                {
                    operation = new Operation()
                    {
                        Name = item.Operation_Name,
                        //...
                        CreatedDate = now
                    };
                    await operationService.AddAsync(operation);
                    operation = await operationService.GetAsync(o => o.Name == item.Operation_Name);//eklenenin Id bilgisini çek
                }
            }
            
        }
    }
}
