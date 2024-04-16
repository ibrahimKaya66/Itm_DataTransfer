using DataTransfer.Business.Services.Abstract;
using DataTransfer.Dal.Context;
using DataTransfer.Dal.Repositories.Concrete;
using DataTransfer.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataTransfer.Business.Services.Concrete
{
    public class CustomerService : RepositoryItm<Customer>, ICustomerService
    {
        private AppDbContext _context;

        public CustomerService(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public List<Customer> GetAll()
        {
            var models = base.GetAll();
            models = _context.Customers.Include(m => m.Country).ToList();

            return models;
        }
        public async Task<Customer> GetAsync(int id)
        {
            var model = await base.GetAsync(id);
            model = await _context.Customers.Include(m => m.Country).SingleOrDefaultAsync(m => m.Id == id);

            return model;
        }
        public async Task<Customer> GetAsync(Expression<Func<Customer, bool>> filter)
        {
            var model = await base.GetAsync(filter);
            model = await _context.Customers.Include(m => m.Country).SingleOrDefaultAsync(filter);

            return model;
        }
    }
}
