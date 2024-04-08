using DataTransfer.Business.Services.Abstract;
using DataTransfer.Dal.Context;
using DataTransfer.Dal.Repositories.Concrete;
using DataTransfer.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataTransfer.Business.Services.Concrete
{
    public class OperationPerformanceService : RepositoryItm<OperationPerformance>, IOperationPerformanceService
    {
        private AppDbContext _context;
        public OperationPerformanceService(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public List<OperationPerformance> GetAll()
        {
            var models = base.GetAll();
            models = _context.OperationPerformances
                .Include(m => m.Operation)
                .Include(m => m.Operator)
                .Include(m => m.Line)
                .ToList();

            return models;
        }
        public async Task<OperationPerformance> GetAsync(int id)
        {
            var model = await base.GetAsync(id);
            model = await _context.OperationPerformances
                .Include(m => m.Operation)
                .Include(m => m.Operator)
                .Include(m => m.Line)
                .SingleOrDefaultAsync(m => m.Id == id);

            return model;
        }
        public async Task<OperationPerformance> GetAsync(Expression<Func<OperationPerformance, bool>> filter)
        {
            var model = await base.GetAsync(filter);
            model = await _context.OperationPerformances
                .Include(m => m.Operation)
                .Include(m => m.Operator)
                .Include(m => m.Line)
                .SingleOrDefaultAsync(filter);

            return model;
        }
    }
}
