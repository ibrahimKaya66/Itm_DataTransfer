using DataTransfer.Business.Services.Abstract;
using DataTransfer.Dal.Context;
using DataTransfer.Dal.Repositories.Concrete;
using DataTransfer.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataTransfer.Business.Services.Concrete
{
    public class Style_OperationService : RepositoryItm<Style_Operation>, IStyle_OperationService
    {
        private AppDbContext _context;
        public Style_OperationService(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public List<Style_Operation> GetAll()
        {
            var models = base.GetAll();
            models = _context.Style_Operations
                .Include(m => m.Style)
                .Include(m => m.Operation)
                .ToList();

            return models;
        }
        public async Task<Style_Operation> GetAsync(int id)
        {
            var model = await base.GetAsync(id);
            model = await _context.Style_Operations
                .Include(m => m.Style)
                .Include(m => m.Operation)
                .SingleOrDefaultAsync(m => m.Id == id);

            return model;
        }
        public async Task<Style_Operation> GetAsync(Expression<Func<Style_Operation, bool>> filter)
        {
            var model = await base.GetAsync(filter);
            model = await _context.Style_Operations
                .Include(m => m.Style)
                .Include(m => m.Operation)
                .SingleOrDefaultAsync(filter);

            return model;
        }
    }
}
