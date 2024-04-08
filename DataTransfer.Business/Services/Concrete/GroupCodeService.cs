using DataTransfer.Business.Services.Abstract;
using DataTransfer.Dal.Context;
using DataTransfer.Dal.Repositories.Concrete;
using DataTransfer.Model.Entities;

namespace DataTransfer.Business.Services.Concrete
{
    public class GroupCodeService : RepositoryItm<GroupCode>, IGroupCodeService
    {
        public GroupCodeService(AppDbContext context) : base(context)
        {
        }
    }
}