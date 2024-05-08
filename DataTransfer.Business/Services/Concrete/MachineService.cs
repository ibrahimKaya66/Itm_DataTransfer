using DataTransfer.Business.Services.Abstract;
using DataTransfer.Dal.Context;
using DataTransfer.Dal.Repositories.Concrete;
using DataTransfer.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataTransfer.Business.Services.Concrete
{
    internal class MachineService : RepositoryItm<Machine>, IMachineService
    {
        private AppDbContext _context;

        public MachineService(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public List<Machine> GetAll()
        {
            var models = base.GetAll();
            models = _context.Machines.Include(m => m.MachineGroup).ToList();

            return models;
        }
        public async Task<Machine> GetAsync(int id)
        {
            var model = await base.GetAsync(id);
            model = await _context.Machines.Include(m => m.MachineGroup).SingleOrDefaultAsync(m => m.Id == id);

            return model;
        }
        public async Task<Machine> GetAsync(Expression<Func<Machine, bool>> filter)
        {
            var model = await base.GetAsync(filter);
            model = await _context.Machines.Include(m => m.MachineGroup).SingleOrDefaultAsync(filter);

            return model;
        }
    }
}
