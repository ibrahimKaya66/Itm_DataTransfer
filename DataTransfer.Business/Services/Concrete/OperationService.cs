using ItmProject.Business.Services.Abstract;
using ItmProject.Dal.Context;
using ItmProject.Dal.Repositories.Concrete;
using ItmProject.Model.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataTransfer.Business.Services.Concrete
{
    public class OperationService : RepositoryItm<Operation>, IOperationService
    {
        private ItmDbContext _context;
        public OperationService(ItmDbContext context, IHttpContextAccessor httpContextAccessor) : base(context, httpContextAccessor)
        {
            _context = context;
        }
        public List<Operation> GetAll()
        {
            var models = base.GetAll();
            models = _context.Operations
                .Include(m => m.Type)
                  .ThenInclude(m => m.TypeCode)
                .Include(m => m.Machine)
                  .ThenInclude(m => m.MachineGroup)
                .Include(m => m.OperationGroup)
                  .ThenInclude(m => m.GroupCode)
                .Include(m => m.Department)
                  .ThenInclude(m => m.Factory)
                .Include(m => m.Apparat)
                  .ThenInclude(m => m.Type)
                .ToList();

            return models;
        }
        public async Task<Operation> GetAsync(int id)
        {
            var model = await base.GetAsync(id);
            model = await _context.Operations
                .Include(m => m.Type)
                  .ThenInclude(m => m.TypeCode)
                .Include(m => m.Machine)
                  .ThenInclude(m => m.MachineGroup)
                .Include(m => m.OperationGroup)
                  .ThenInclude(m => m.GroupCode)
                .Include(m => m.Department)
                  .ThenInclude(m => m.Factory)
                .Include(m => m.Apparat)
                  .ThenInclude(m => m.Type)
                .SingleOrDefaultAsync(p => p.Id == id);

            return model;
        }
        public async Task<Operation> GetAsync(Expression<Func<Operation, bool>> filter)
        {
            var model = await base.GetAsync(filter);
            model = await _context.Operations
                .Include(m => m.Type)
                  .ThenInclude(m => m.TypeCode)
                .Include(m => m.Machine)
                  .ThenInclude(m => m.MachineGroup)
                .Include(m => m.OperationGroup)
                  .ThenInclude(m => m.GroupCode)
                .Include(m => m.Department)
                  .ThenInclude(m => m.Factory)
                .Include(m => m.Apparat)
                  .ThenInclude(m => m.Type)
                .SingleOrDefaultAsync(filter);

            return model;
        }
    }
}