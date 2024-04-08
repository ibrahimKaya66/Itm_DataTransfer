using DataTransfer.Business.Services.Abstract;
using DataTransfer.Dal.Context;
using DataTransfer.Dal.Repositories.Concrete;
using DataTransfer.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataTransfer.Business.Services.Concrete
{
    public class OperationService : RepositoryItm<Operation>, IOperationService
    {
        private AppDbContext _context;
        public OperationService(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public List<Operation> GetAll()
        {
            var models = base.GetAll();
            models = _context.Operations
                .Include(m => m.OperationGroup)
                  .ThenInclude(m => m.GroupCode)
                .Include(m => m.Department)
                .ToList();

            return models;
        }
        public async Task<Operation> GetAsync(int id)
        {
            var model = await base.GetAsync(id);
            model = await _context.Operations
                .Include(m => m.OperationGroup)
                  .ThenInclude(m => m.GroupCode)
                .Include(m => m.Department)
                .SingleOrDefaultAsync(p => p.Id == id);

            return model;
        }
        public async Task<Operation> GetAsync(Expression<Func<Operation, bool>> filter)
        {
            var model = await base.GetAsync(filter);
            model = await _context.Operations
                .Include(m => m.OperationGroup)
                  .ThenInclude(m => m.GroupCode)
                .Include(m => m.Department)
                .SingleOrDefaultAsync(filter);

            return model;
        }
    }
}