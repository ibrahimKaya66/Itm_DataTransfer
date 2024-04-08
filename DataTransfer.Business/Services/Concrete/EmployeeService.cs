using DataTransfer.Business.Services.Abstract;
using DataTransfer.Dal.Context;
using DataTransfer.Dal.Repositories.Concrete;
using DataTransfer.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataTransfer.Business.Services.Concrete
{
    public class EmployeeService : RepositoryItm<Employee>, IEmployeeService
    {
        private AppDbContext _context;
        public EmployeeService(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public List<Employee> GetAll()
        {

            var models = base.GetAll();

            models = _context.Employees
                .Include(m => m.Department)
                .ToList();

            return models;
        }
        public async Task<Employee> GetAsync(int id)
        {
            var model = await base.GetAsync(id);
            model = await _context.Employees
                 .Include(m => m.Department)
                .SingleOrDefaultAsync(m => m.Id == id);

            return model;
        }
        public async Task<Employee> GetAsync(Expression<Func<Employee, bool>> filter)
        {
            var model = await base.GetAsync(filter);
            model = await _context.Employees
                 .Include(m => m.Department)
                .SingleOrDefaultAsync(filter);

            return model;
        }
    }
}