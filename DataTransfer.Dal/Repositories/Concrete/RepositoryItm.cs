using DataTransfer.Dal.Context;
using DataTransfer.Dal.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataTransfer.Dal.Repositories.Concrete
{
    public class RepositoryItm<Tentity> : IRepositoryItm<Tentity> where Tentity : class, new()
    {
        private AppDbContext _context;

        public RepositoryItm(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Tentity entity)
        {
            await _context.AddAsync(entity);
            //_context.AddAsync<Tentity>(entity);
            //_context.Set<Tentity>().AddAsync(entity);
            //_context.Entry(entity).State = EntityState.Added;
            await _context.SaveChangesAsync();
        }
        public List<Tentity> GetAll()
        {
            return _context.Set<Tentity>().ToList();
        }
        public async Task<Tentity> GetAsync(int id)
        {
            var entity = await _context.FindAsync<Tentity>(id);
            return entity;
        }
        public async Task<Tentity> GetAsync(Expression<Func<Tentity, bool>> predicate)
        {
            var entity = await _context.Set<Tentity>().SingleOrDefaultAsync<Tentity>(predicate);
            return entity;
        }
        public async Task RemoveAsync(Tentity entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();//gerek olamyabilir
        }
        public async Task UpdateAsync(Tentity entity)
        {
            //_context.Update(entity);
            //_context.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();//gerek olmayabilir
        }
    }
}
