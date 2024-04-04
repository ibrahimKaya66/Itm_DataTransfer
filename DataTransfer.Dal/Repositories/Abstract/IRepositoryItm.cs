using System.Linq.Expressions;

namespace DataTransfer.Dal.Repositories.Abstract
{
    public interface IRepositoryItm<Tentity> where Tentity : class, new()
    {
        Task<Tentity> GetAsync(int id);
        Task<Tentity> GetAsync(Expression<Func<Tentity, bool>> predicate);
        List<Tentity> GetAll();
        Task AddAsync(Tentity entity);
        Task UpdateAsync(Tentity entity);
        Task RemoveAsync(Tentity entity);
    }
}
