using DataTransfer.Business.Services.Abstract;
using DataTransfer.Dal.Context;
using DataTransfer.Dal.Repositories.Concrete;
using DataTransfer.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataTransfer.Business.Services.Concrete
{
    public class LineService : RepositoryItm<Line>, ILineService
    {
        private AppDbContext _context;
        public LineService(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public List<Line> GetAll()
        {
            var models = base.GetAll();
            models = _context.Lines
                .Include(m => m.Department)
                .ToList();

            return models;
        }
        public async Task<Line> GetAsync(int id)
        {
            var model = await base.GetAsync(id);
            model = await _context.Lines
                .Include(m => m.Department)
                .SingleOrDefaultAsync(p => p.Id == id);

            return model;
        }
        public async Task<Line> GetAsync(Expression<Func<Line, bool>> filter)
        {
            var model = await base.GetAsync(filter);
            model = await _context.Lines
                .Include(m => m.Department)
                .SingleOrDefaultAsync(filter);

            return model;
        }
    }
}