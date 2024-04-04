using ItmProject.Business.Services.Abstract;
using ItmProject.Dal.Context;
using ItmProject.Dal.Repositories.Concrete;
using ItmProject.Model.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataTransfer.Business.Services.Concrete
{
    public class DepartmentService : RepositoryItm<Department>, IDepartmentService
    {
        private ItmDbContext _context;
        public DepartmentService(ItmDbContext context, IHttpContextAccessor httpContextAccessor) : base(context, httpContextAccessor)
        {
            _context = context;
        }
        public List<Department> GetAll()
        {
            var models = base.GetAll();
            models = _context.Departments
                .Include(m => m.Factory)
                  .ThenInclude(m => m.Country)
                .ToList();

            return models;
        }
        public async Task<Department> GetAsync(int id)
        {
            var model = await base.GetAsync(id);
            model = await _context.Departments
                 .Include(m => m.Factory)
                  .ThenInclude(m => m.Country)
                .SingleOrDefaultAsync(m => m.Id == id);

            return model;
        }
        public async Task<Department> GetAsync(Expression<Func<Department, bool>> filter)
        {
            var model = await base.GetAsync(filter);
            model = await _context.Departments
                 .Include(m => m.Factory)
                  .ThenInclude(m => m.Country)
                .SingleOrDefaultAsync(filter);

            return model;
        }
    }
}