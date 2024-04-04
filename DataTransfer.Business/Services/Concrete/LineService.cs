using ItmProject.Business.Services.Abstract;
using ItmProject.Dal.Context;
using ItmProject.Dal.Repositories.Concrete;
using ItmProject.Model.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataTransfer.Business.Services.Concrete
{
    public class LineService : RepositoryItm<Line>, ILineService
    {
        private ItmDbContext _context;
        public LineService(ItmDbContext context, IHttpContextAccessor httpContextAccessor) : base(context, httpContextAccessor)
        {
            _context = context;
        }
        public List<Line> GetAll()
        {
            var models = base.GetAll();
            models = _context.Lines
                .Include(m => m.Department)
                    .ThenInclude(m => m.Factory)
                .ToList();

            return models;
        }
        public async Task<Line> GetAsync(int id)
        {
            var model = await base.GetAsync(id);
            model = await _context.Lines
                .Include(m => m.Department)
                    .ThenInclude(m => m.Factory).SingleOrDefaultAsync(p => p.Id == id);

            return model;
        }
        public async Task<Line> GetAsync(Expression<Func<Line, bool>> filter)
        {
            var model = await base.GetAsync(filter);
            model = await _context.Lines.Include(m => m.Department).ThenInclude(m => m.Factory).SingleOrDefaultAsync(filter);

            return model;
        }
    }
}