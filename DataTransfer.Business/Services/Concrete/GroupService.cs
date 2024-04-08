using DataTransfer.Business.Services.Abstract;
using DataTransfer.Dal.Context;
using DataTransfer.Dal.Repositories.Concrete;
using DataTransfer.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataTransfer.Business.Services.Concrete
{
    public class GroupService : RepositoryItm<Group>, IGroupService
    {
        private AppDbContext _context;
        public GroupService(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public List<Group> GetAll()
        {
            var models = base.GetAll();
            models = _context.Groups
                    .Include(m => m.GroupCode).ToList();

            return models;
        }
        public async Task<Group> GetAsync(int id)
        {
            var model = await base.GetAsync(id);
            model = await _context.Groups
                    .Include(m => m.GroupCode).SingleOrDefaultAsync(m => m.Id == id);

            return model;
        }
        public async Task<Group> GetAsync(Expression<Func<Group, bool>> filter)
        {
            var model = await base.GetAsync(filter);
            model = await _context.Groups
                .Include(m => m.GroupCode).SingleOrDefaultAsync(filter);

            return model;
        }
    }
}