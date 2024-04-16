using DataTransfer.Business.Services.Abstract;
using DataTransfer.Dal.Context;
using DataTransfer.Dal.Repositories.Concrete;
using DataTransfer.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataTransfer.Business.Services.Concrete
{
    public class StyleService : RepositoryItm<Style>, IStyleService
    {
        private AppDbContext _context;

        public StyleService(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public List<Style> GetAll()
        {
            var models = base.GetAll();
            models = _context.Styles
                    .Include(m => m.Customer)
                    .ToList();

            return models;
        }
        public async Task<Style> GetAsync(int id)
        {
            var model = await base.GetAsync(id);
            model = await _context.Styles
                    .Include(m => m.Customer)
                    .SingleOrDefaultAsync(m => m.Id == id);

            return model;
        }
        public async Task<Style> GetAsync(Expression<Func<Style, bool>> filter)
        {
            var model = await base.GetAsync(filter);
            model = await _context.Styles
                    .Include(m => m.Customer)
                    .SingleOrDefaultAsync(filter);

            return model;
        }
    }
}
