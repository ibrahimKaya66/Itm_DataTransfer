using DataTransfer.Business.Services.Abstract;
using DataTransfer.Dal.Context;
using DataTransfer.Dal.Repositories.Concrete;
using DataTransfer.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataTransfer.Business.Services.Concrete
{
    public class FactoryService : RepositoryItm<Factory>, IFactoryService
    {
        private AppDbContext _context;
        public FactoryService(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public List<Factory> GetAll()
        {
            var models = base.GetAll();
            models = _context.Factories.Include(m => m.Country).ToList();

            return models;
        }
        public async Task<Factory> GetAsync(int id)
        {
            var model = await base.GetAsync(id);
            model = await _context.Factories.Include(m => m.Country).SingleOrDefaultAsync(m => m.Id == id);

            return model;
        }
        public async Task<Factory> GetAsync(Expression<Func<Factory, bool>> filter)
        {
            var model = await base.GetAsync(filter);
            model = await _context.Factories.Include(m => m.Country).SingleOrDefaultAsync(filter);

            return model;
        }
    }
}
