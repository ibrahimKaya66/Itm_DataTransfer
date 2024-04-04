﻿using DataTransfer.Business.Methods.Abstract;
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
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<IGroupCodeService, GroupCodeService>();
            services.AddScoped<ILineService, LineService>();
            services.AddScoped<IOperationService, OperationService>();
            services.AddScoped<IOperationPerformanceService, OperationPerformanceService>();

            //Methods
            services.AddScoped<IDepartmentMethod, DepartmentMethod>();
            services.AddScoped<IEmployeeMethod, EmployeeMethod>();
            services.AddScoped<IGroupMethod, GroupMethod>();
            services.AddScoped<IGroupCodeMethod, GroupCodeMethod>();
            services.AddScoped<ILineMethod, LineMethod>();            
            services.AddScoped<IOperationMethod, OperationMethod>();
            services.AddScoped<IOperationPerformanceMethod, OperationPerformanceMethod>();

            var assembly = Assembly.GetExecutingAssembly();

            return services;
        }
    }
}