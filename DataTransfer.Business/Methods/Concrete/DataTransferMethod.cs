﻿using DataTransfer.Business.Methods.Abstract;
using DataTransfer.Business.Services.Abstract;
using DataTransfer.Model.Ado.Net;
using DataTransfer.Model.Entities;

namespace DataTransfer.Business.Methods.Concrete
{
    public class DataTransferMethod : IDataTransferMethod
    {
        private readonly ICustomerService customerService;
        private readonly IDepartmentService departmentService;
        private readonly IEmployeeService employeeService;
        private readonly IFactoryService factoryService;
        private readonly IGroupCodeService groupCodeService;
        private readonly IGroupService groupService;
        private readonly IJobService jobService;
        private readonly ILineService lineService;
        private readonly IOperationPerformanceService operationPerformanceService;
        private readonly IOperationService operationService;
        private readonly IStyleService styleService;
        private readonly IStyle_OperationService style_OperationService;

        public DataTransferMethod(ICustomerService customerService, IDepartmentService departmentService, IEmployeeService employeeService, IFactoryService factoryService, IGroupCodeService groupCodeService, IGroupService groupService, IJobService jobService, ILineService lineService, IOperationPerformanceService operationPerformanceService, IOperationService operationService, IStyleService styleService, IStyle_OperationService style_OperationService)
        {
            this.customerService = customerService;
            this.departmentService = departmentService;
            this.employeeService = employeeService;
            this.factoryService = factoryService;
            this.groupCodeService = groupCodeService;
            this.groupService = groupService;
            this.jobService = jobService;
            this.lineService = lineService;
            this.operationPerformanceService = operationPerformanceService;
            this.operationService = operationService;
            this.styleService = styleService;
            this.style_OperationService = style_OperationService;
        }

        public async Task DataTransfer(List<OperatorPerformance> model)
        {
            Department department = new Department();
            Employee employee = new Employee();
            GroupCode groupCode = new GroupCode();
            Group group = new Group();
            Job job = new Job();
            Line line = new Line();
            OperationPerformance? operationPerformance = new OperationPerformance();
            Operation operation = new Operation();

            DateTime utcNow = DateTime.UtcNow;
            var factory = factoryService.GetAll().FirstOrDefault();
            var utc = Convert.ToDouble(factory?.Country.UtcOffset ?? 3);
            DateTime now = utcNow.AddHours(utc);

            foreach (var item in model)
            {
                //department add
                department = await departmentService.GetAsync(d => d.Name.ToLower() == item.Department_Name.ToLower());
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
                job = await jobService.GetAsync(j => j.Name.ToLower() == item.Job_Name.ToLower());
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
                employee = await employeeService.GetAsync(e => e.SourceId == item.EmployeeId);
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
                groupCode = await groupCodeService.GetAsync(g => g.Name == "operasyon");

                //group add
                group = await groupService.GetAsync(g => g.Name.ToLower().Contains(item.Group_Name ?? "") == true);
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
                line = await lineService.GetAsync(l => l.Name.ToLower() == item.Line_Name.ToLower());
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
                operation = await operationService.GetAsync(o => o.Name.ToLower() == item.Operation_Name.ToLower());
                if (operation == null)
                {
                    int typeId = 0;
                    if(item.Operation_Type?.ToLower() == "hand")
                        typeId = 1;
                    else if(item.Operation_Type?.ToLower() == "mach")
                        typeId= 2;

                    operation = new Operation()
                    {
                        Name = item.Operation_Name,
                        TypeId = typeId,
                        OperationGroupId = group.Id,
                        DepartmentId = department.Id,
                        TimeSecond = item.TimeSecond,
                        CreatedDate = now
                    };
                    await operationService.AddAsync(operation);
                    operation = await operationService.GetAsync(o => o.Name == item.Operation_Name);//eklenenin Id bilgisini çek
                }

                //operationPerformance add
                operationPerformance =  operationPerformanceService.GetAll().Where(op => op.OperationId == operation.Id && op.OperatorId == employee.Id && op.LineId == line.Id && op.Performance == item.Performance).FirstOrDefault();
                if (operationPerformance == null)
                {
                    operationPerformance = new OperationPerformance()
                    {
                        OperationId = operation.Id,
                        OperatorId = employee.Id,
                        LineId = line.Id,
                        Performance = item.Performance,
                        Date_ = now,
                        CreatedDate = now
                    };
                    await operationPerformanceService.AddAsync(operationPerformance);
                    operationPerformance = await operationPerformanceService.GetAsync(op => op.OperationId == operation.Id && op.OperatorId == employee.Id && op.LineId == line.Id);//eklenenin Id bilgisini çek
                }
            }
            
        }
        public async Task StyleOperations(string styleName, List<StyleOperation> models)
        {
            DateTime utcNow = DateTime.UtcNow;
            var factory = factoryService.GetAll().FirstOrDefault();
            var utc = Convert.ToDouble(factory?.Country.UtcOffset ?? 3);
            DateTime now = utcNow.AddHours(utc);

            Style style = new Style();
            Customer customer = new Customer();
            Department department = new Department();
            Group group = new Group();
            //department add
            var item = models.FirstOrDefault();
            style = await styleService.GetAsync(s => s.Name.ToLower() == styleName.ToLower() && s.ReferanceNo.ToLower() == item.StyleCode.ToLower());
            if (style == null)
            {
                customer = await customerService.GetAsync(c=>c.Name.ToLower() == item.CustomerName.ToLower());
                if(customer == null)
                {
                    customer = new Customer()
                    {
                        Name = item.CustomerName,
                        CountryId = 1,
                        CreatedDate = now
                    };
                    await customerService.AddAsync(customer);
                    customer = await customerService.GetAsync(c => c.Name.ToLower() == item.CustomerName.ToLower());
                }
                style = new Style()
                {
                    Name = styleName,
                    ReferanceNo = item?.StyleCode,
                    CustomerId = customer.Id,
                    StyleGroupId = 8,//bay
                    SeasonGroupId = 14,//yaz
                    CatalogGroupId = 11,//t-shirt
                    SetGroupId = 16,//alt-üst
                    CreatedDate = now
                };
                await styleService.AddAsync(style);
                style = await styleService.GetAsync(s => s.Name.ToLower() == styleName.ToLower() && s.ReferanceNo.ToLower() == item.StyleCode.ToLower());//eklenenin Id bilgisini çek
            }
            foreach (var model in models)
            {
                department = await departmentService.GetAsync(d => d.Name.ToLower() == item.DepartmentName.ToLower());
                if (department == null)
                {
                    department = new Department()
                    {
                        Name = item.DepartmentName,
                        FactoryId = factory?.Id ?? 1,
                        CreatedDate = now
                    };
                    await departmentService.AddAsync(department);
                    department = await departmentService.GetAsync(d => d.Name == item.DepartmentName);//eklenenin Id bilgisini çek
                }

                group = await groupService.GetAsync(d => d.Name.ToLower() == item.OperationGroupName.ToLower());
                if (group == null)
                {
                    group = new Group()
                    {
                        Name = item.OperationGroupName,
                        GroupCodeId = 4,//catalog group
                        CreatedDate = now
                    };
                    await groupService.AddAsync(group);
                    group = await groupService.GetAsync(d => d.Name.ToLower() == item.OperationGroupName.ToLower());//eklenenin Id bilgisini çek
                }

                var operation = await operationService.GetAsync(o => o.Name.ToLower() == model.OperationName && o.TimeSecond == model.TimeSecond);
                if (operation == null)
                {
                    int typeId = 0;
                    if (item.OperationType?.ToLower() == "hand")
                        typeId = 1;
                    else if (item.OperationType?.ToLower() == "mach")
                        typeId = 2;

                    operation = new Operation()
                    {
                        Name = model.OperationName,
                        TypeId = typeId,
                        OperationGroupId = group.Id,
                        DepartmentId = department.Id,
                        TimeSecond = model.TimeSecond,
                        CreatedDate = now
                    };
                    await operationService.AddAsync(operation);
                    operation = await operationService.GetAsync(o => o.Name.ToLower() == model.OperationName && o.TimeSecond == model.TimeSecond);
                }
                var style_Operation = await style_OperationService.GetAsync(so => so.StyleId == style.Id && so.OperationId == operation.Id && so.EntityOrder == model.EntityOrder);
                if (style_Operation == null) 
                {
                    style_Operation = new Style_Operation()
                    {
                        StyleId = style.Id,
                        OperationId = operation.Id,
                        EntityOrder = model.EntityOrder,
                        CreatedDate = now
                    };
                    await style_OperationService.AddAsync(style_Operation);
                }
            }
        }
    }
}
