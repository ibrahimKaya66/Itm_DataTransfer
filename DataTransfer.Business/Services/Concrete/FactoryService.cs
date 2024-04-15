using DataTransfer.Business.Services.Abstract;
using DataTransfer.Dal.Context;
using DataTransfer.Dal.Repositories.Concrete;
using DataTransfer.Model.Entities;

namespace DataTransfer.Business.Services.Concrete
{
    public class FactoryService : RepositoryItm<Factory>, IFactoryService
    {
        public FactoryService(AppDbContext context) : base(context)
        {
        }
    }
}
