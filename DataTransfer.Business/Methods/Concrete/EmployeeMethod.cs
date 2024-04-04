using AutoMapper;
using ItmProject.Business.Methods.Abstract;
using ItmProject.Business.Models;
using ItmProject.Business.Services.Abstract;
using ItmProject.Model.DTOs;
using ItmProject.Model.Models.Entities;

namespace DataTransfer.Business.Methods.Concrete
{
    public class EmployeeMethod : IEmployeeMethod
    {
        private readonly IEmployeeService employeeService;
        private readonly IMapper mapper;
        private readonly IEmployeeDetailService employeeDetailService;
        private readonly IEmployeeSalaryService employeeSalaryService;
        private readonly IFactoryService factoryService;

        public EmployeeMethod(IEmployeeService employeeService, IMapper mapper, IEmployeeDetailService employeeDetailService, IEmployeeSalaryService employeeSalaryService, IFactoryService factoryService)
        {
            this.employeeService = employeeService;
            this.mapper = mapper;
            this.employeeDetailService = employeeDetailService;
            this.employeeSalaryService = employeeSalaryService;
            this.factoryService = factoryService;
        }
        public List<EmployeeDTO>? Get()
        {
            try
            {
                var models = employeeService.GetAll().Where(m => m.IsDeleted == false && m.IsWork == true).ToList();
                var responseDtos = new List<EmployeeDTO>();
                foreach (var model in models)
                {
                    var responseDto = mapper.Map<EmployeeDTO>(model);
                    responseDtos.Add(responseDto);
                }

                return responseDtos;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<EmployeeDTO?> Get(int id)
        {
            var model = await employeeService.GetAsync(id);
            if (model != null)
            {
                var responseDto = mapper.Map<EmployeeDTO>(model);
                return responseDto;
            }

            else
                return null;
        }

        public async Task<EmployeeDTO?> Post(EmployeeDTO model)
        {
            DateTime utcNow = DateTime.UtcNow;
            var factory = factoryService.GetAll().FirstOrDefault();
            var utc = Convert.ToDouble(factory?.Country.UtcOffset ?? 3);
            DateTime now = utcNow.AddHours(utc);

            if (!string.IsNullOrEmpty(model.UserName))
            {
                var employee = await employeeService.GetAsync(e => e.UserName == model.UserName);

                if (employee != null)
                    return null;
            }
            var entity = mapper.Map<Employee>(model); // DTO'yu Employee'a dönüştür
            entity.CreatedBy = "apiUser";
            entity.CreatedDate = now;
            try
            {
                await employeeService.AddAsync(entity);
                var responseDto = mapper.Map<EmployeeDTO>(entity); // Employee'ı DTO'ya dönüştür
                return responseDto;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<EmployeeDTO?> Put(int id, EmployeeDTO model)
        {
            DateTime utcNow = DateTime.UtcNow;
            var factory = factoryService.GetAll().FirstOrDefault();
            var utc = Convert.ToDouble(factory?.Country.UtcOffset ?? 3);
            DateTime now = utcNow.AddHours(utc);

            model.Id = id;
            var entity = await employeeService.GetAsync(id);
            if (entity == null)
            {
                return null;
            }
            if (!string.IsNullOrEmpty(model.UserName))
            {
                var employee = await employeeService.GetAsync(e => e.UserName == model.UserName && e.Id != id);
                if (employee != null)
                    return null;
            }
            mapper.Map(model, entity); // EmployeeDTO nesnesini entity'ye dönüştür
            entity.ModifiedBy = "apiUser";
            entity.ModifiedDate = now;
            try
            {
                await employeeService.UpdateAsync(entity);
                var responseDto = mapper.Map<EmployeeDTO>(entity); // Güncellenmiş entity'yi EmployeeDTO'ya dönüştür

                return responseDto;
            }
            catch (Exception)
            {

                return null;
            }
        }
        public async Task<EmployeeDTO?> Login(EmployeeLogin model)
        {
            try
            {
                var employee = await employeeService.GetAsync(m => m.UserName == model.Username);
                if (employee != null)
                {
                    if (employee.Password == model.Password)
                    {
                        var responseDto = mapper.Map<EmployeeDTO>(employee);
                        return responseDto;
                    }

                    else
                        return null;
                }

                else
                    return null;
            }
            catch (Exception)
            {

                return null;
            }
        }
        public async Task<EmployeeDetailDTO?> GetEmployeeDetail(int? employeeId)
        {
            if (employeeId != null)
            {
                var employee = await employeeService.GetAsync(Convert.ToInt32(employeeId));
                if (employee != null)
                {
                    var employeeDetail = await employeeDetailService.GetAsync(m => m.Id == employee.EmployeeDetailId);
                    var model = mapper.Map<EmployeeDetailDTO>(employeeDetail);
                    return model;
                }

                else
                    return null;
            }
            else
            {
                return null;
            }
        }
        public async Task<EmployeeSalaryDTO?> GetEmployeeSalary(int? employeeId)
        {
            if (employeeId != null)
            {
                var employee = await employeeService.GetAsync(Convert.ToInt32(employeeId));
                if (employee != null)
                {
                    var employeeDetail = await employeeSalaryService.GetAsync(m => m.Id == employee.EmployeeSalaryId);
                    var model = mapper.Map<EmployeeSalaryDTO>(employeeDetail);
                    return model;
                }

                else
                    return null;
            }
            else
            {
                return null;
            }
        }
        public async Task<EmployeeDetailDTO?> PostEmployeeDetail(int employeeId, EmployeeDetailDTO model)
        {
            try
            {
                DateTime utcNow = DateTime.UtcNow;
                var factory = factoryService.GetAll().FirstOrDefault();
                var utc = Convert.ToDouble(factory?.Country.UtcOffset ?? 3);
                DateTime now = utcNow.AddHours(utc);

                var employeeDetail = mapper.Map<EmployeeDetail>(model);
                employeeDetail.CreatedBy = "apiUser";
                employeeDetail.CreatedDate = now;
                await employeeDetailService.AddAsync(employeeDetail);

                var lastEmployeeDetail = employeeDetailService.GetAll().OrderByDescending(ed => ed.Id).FirstOrDefault();

                var employee = await employeeService.GetAsync(employeeId);
                employee.EmployeeDetailId = lastEmployeeDetail?.Id;
                employee.ModifiedBy = "apiUser";
                employee.ModifiedDate = now;
                await employeeService.UpdateAsync(employee);
                return model;
            }
            catch (Exception)
            {
                // Hata durumunda uygun işlemi gerçekleştirin (loglama, hata mesajı dönme, vb.)
                return null;
            }
        }
        public async Task<EmployeeSalaryDTO?> PostEmployeeSalary(int employeeId, EmployeeSalaryDTO model)
        {
            try
            {
                DateTime utcNow = DateTime.UtcNow;
                var factory = factoryService.GetAll().FirstOrDefault();
                var utc = Convert.ToDouble(factory?.Country.UtcOffset ?? 3);
                DateTime now = utcNow.AddHours(utc);

                var employeeSalary = mapper.Map<EmployeeSalary>(model);
                employeeSalary.CreatedBy = "apiUser";
                employeeSalary.CreatedDate = now;
                await employeeSalaryService.AddAsync(employeeSalary);

                var lastEmployeeSalary = employeeSalaryService.GetAll().OrderByDescending(ed => ed.Id).FirstOrDefault();

                var employee = await employeeService.GetAsync(employeeId);
                employee.EmployeeSalaryId = lastEmployeeSalary?.Id;
                employee.ModifiedBy = "apiUser";
                employee.ModifiedDate = now;
                await employeeService.UpdateAsync(employee);
                return model;
            }
            catch (Exception)
            {
                // Hata durumunda uygun işlemi gerçekleştirin (loglama, hata mesajı dönme, vb.)
                return null;
            }
        }
        public async Task<EmployeeDetailDTO?> PutEmployeeDetail(int employeeId, EmployeeDetailDTO model)
        {
            DateTime utcNow = DateTime.UtcNow;
            var factory = factoryService.GetAll().FirstOrDefault();
            var utc = Convert.ToDouble(factory?.Country.UtcOffset ?? 3);
            DateTime now = utcNow.AddHours(utc);

            var employee = await employeeService.GetAsync(employeeId);

            if (employee == null)
            {
                return null;
            }
            var employeeDetail = await employeeDetailService.GetAsync(m => m.Id == employee.EmployeeDetailId);
            if (employeeDetail == null)
            {
                return null;
            }
            mapper.Map(model, employeeDetail);
            employeeDetail.ModifiedBy = "apiUser";
            employeeDetail.ModifiedDate = now;

            await employeeDetailService.UpdateAsync(employeeDetail);
            return model;
        }
        public async Task<EmployeeSalaryDTO?> PutEmployeeSalary(int employeeId, EmployeeSalaryDTO model)
        {
            DateTime utcNow = DateTime.UtcNow;
            var factory = factoryService.GetAll().FirstOrDefault();
            var utc = Convert.ToDouble(factory?.Country.UtcOffset ?? 3);
            DateTime now = utcNow.AddHours(utc);

            var employee = await employeeService.GetAsync(employeeId);

            if (employee == null)
            {
                return null;
            }
            var employeeSalary = await employeeSalaryService.GetAsync(m => m.Id == employee.EmployeeSalaryId);
            if (employeeSalary == null)
            {
                return null;
            }
            mapper.Map(model, employeeSalary);
            employeeSalary.ModifiedBy = "apiUser";
            employeeSalary.ModifiedDate = now;

            await employeeSalaryService.UpdateAsync(employeeSalary);
            return model;
        }
        public async Task<EmployeeDTO?> Delete(int id)
        {
            var model = await employeeService.GetAsync(id);
            var modelDetail = await employeeDetailService.GetAsync(m => m.Id == model.EmployeeDetailId);
            var modelSalary = await employeeSalaryService.GetAsync(m => m.Id == model.EmployeeSalaryId);
            if (model != null)
            {
                await employeeSalaryService.RemoveAsync(modelSalary);
                await employeeDetailService.RemoveAsync(modelDetail);
                await employeeService.RemoveAsync(model);
                var responseDto = mapper.Map<EmployeeDTO>(model);
                return responseDto;
            }
            else
                return null;
        }
    }
}
