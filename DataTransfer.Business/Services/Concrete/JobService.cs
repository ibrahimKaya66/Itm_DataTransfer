using DataTransfer.Business.Services.Abstract;
using DataTransfer.Dal.Context;
using DataTransfer.Dal.Repositories.Concrete;
using DataTransfer.Model.Entities;

namespace DataTransfer.Business.Services.Concrete
{
    public class JobService : RepositoryItm<Job>, IJobService
    {
        public JobService(AppDbContext context) : base(context)
        {
        }
    }
}
