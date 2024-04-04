using ItmProject.Business.Services.Abstract;
using ItmProject.Dal.Context;
using ItmProject.Dal.Repositories.Concrete;
using ItmProject.Model.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataTransfer.Business.Services.Concrete
{
    public class EmployeeService : RepositoryItm<Employee>, IEmployeeService
    {
        private ItmDbContext _context;
        public EmployeeService(ItmDbContext context, IHttpContextAccessor httpContextAccessor) : base(context, httpContextAccessor)
        {
            _context = context;
        }
        public List<Employee> GetAll()
        {

            var models = base.GetAll();

            models = _context.Employees
                .Include(m => m.Department)
                  .ThenInclude(m => m.Factory)
                .Include(m => m.Job)
                .Include(m => m.ExpenseType)
                  .ThenInclude(m => m.TypeCode)
                .Include(m => m.EmployeeDetail)
                .Include(m => m.EmployeeSalary)
                .ToList();

            return models;
        }
        public async Task<Employee> GetAsync(int id)
        {
            var model = await base.GetAsync(id);
            model = await _context.Employees
                 .Include(m => m.Department)
                  .ThenInclude(m => m.Factory)
                .Include(m => m.Job)
                .Include(m => m.ExpenseType)
                  .ThenInclude(m => m.TypeCode)
                .Include(m => m.EmployeeDetail)
                .Include(m => m.EmployeeSalary)
                .SingleOrDefaultAsync(m => m.Id == id);

            return model;
        }
        public async Task<Employee> GetAsync(Expression<Func<Employee, bool>> filter)
        {
            var model = await base.GetAsync(filter);
            model = await _context.Employees
                 .Include(m => m.Department)
                  .ThenInclude(m => m.Factory)
                .Include(m => m.Job)
                .Include(m => m.ExpenseType)
                  .ThenInclude(m => m.TypeCode)
                .Include(m => m.EmployeeDetail)
                .Include(m => m.EmployeeSalary)
                .SingleOrDefaultAsync(filter);

            return model;
        }
    }
}