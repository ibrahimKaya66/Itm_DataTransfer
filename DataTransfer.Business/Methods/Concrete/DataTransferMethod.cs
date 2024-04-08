using DataTransfer.Business.Methods.Abstract;
using DataTransfer.Business.Services.Abstract;
using DataTransfer.Model.Ado.Net;

namespace DataTransfer.Business.Methods.Concrete
{
    public class DataTransferMethod : IDataTransferMethod
    {
        private readonly IDepartmentService departmentService;
        private readonly IEmployeeService employeeService;
        private readonly IGroupCodeService groupCodeService;
        private readonly IGroupService groupService;
        private readonly ILineService lineService;
        private readonly IOperationPerformanceService operationPerformanceService;
        private readonly IOperationService operationService;

        public DataTransferMethod(IDepartmentService departmentService, IEmployeeService employeeService, IGroupCodeService groupCodeService, IGroupService groupService, ILineService lineService, IOperationPerformanceService operationPerformanceService, IOperationService operationService)
        {
            this.departmentService = departmentService;
            this.employeeService = employeeService;
            this.groupCodeService = groupCodeService;
            this.groupService = groupService;
            this.lineService = lineService;
            this.operationPerformanceService = operationPerformanceService;
            this.operationService = operationService;
        }

        public async Task DataTransfer(List<OperatorPerformance> model)
        {
            
        }
    }
}
