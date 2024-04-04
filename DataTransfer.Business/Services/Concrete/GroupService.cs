using ItmProject.Business.Services.Abstract;
using ItmProject.Dal.Context;
using ItmProject.Dal.Repositories.Concrete;
using ItmProject.Model.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataTransfer.Business.Services.Concrete
{
    public class GroupService : RepositoryItm<Group>, IGroupService
    {
        private ItmDbContext _context;
        public GroupService(ItmDbContext context, IHttpContextAccessor httpContextAccessor) : base(context, httpContextAccessor)
        {
            _context = context;
        }
        public List<Group> GetAll()
        {
            var models = base.GetAll();
            models = _context.Groups.Include(m => m.GroupCode).ToList();

            return models;
        }
        public async Task<Group> GetAsync(int id)
        {
            var model = await base.GetAsync(id);
            model = await _context.Groups.Include(m => m.GroupCode).SingleOrDefaultAsync(m => m.Id == id);

            return model;
        }
        public async Task<Group> GetAsync(Expression<Func<Group, bool>> filter)
        {
            var model = await base.GetAsync(filter);
            model = await _context.Groups.Include(m => m.GroupCode).SingleOrDefaultAsync(filter);

            return model;
        }
    }
}