using DataTransfer.Business.Methods.Abstract;
using DataTransfer.Business.Methods.Concrete;
using DataTransfer.Business.Services.Abstract;
using DataTransfer.Business.Services.Concrete;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DataTransfer.Business.Extensions
{
    public static class BusinessExtension
    {
        public static IServiceCollection LoadBusinessLayerExtension(this IServiceCollection services)
        {
            //Services
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IFactoryService, FactoryService>();
            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<IGroupCodeService, GroupCodeService>();
            services.AddScoped<IJobService, JobService>();
            services.AddScoped<ILineService, LineService>();
            services.AddScoped<IMachineService, MachineService>();
            services.AddScoped<IOperationService, OperationService>();
            services.AddScoped<IOperationPerformanceService, OperationPerformanceService>();
            services.AddScoped<IStyleService, StyleService>();
            services.AddScoped<IStyle_OperationService, Style_OperationService>();

            //Methods
            services.AddScoped<IDataTransferMethod, DataTransferMethod>();

            var assembly = Assembly.GetExecutingAssembly();

            return services;
        }
    }
}