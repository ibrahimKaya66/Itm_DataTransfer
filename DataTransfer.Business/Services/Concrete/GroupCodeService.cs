using ItmProject.Business.Services.Abstract;
using ItmProject.Dal.Context;
using ItmProject.Dal.Repositories.Concrete;
using ItmProject.Model.Models.Entities;
using Microsoft.AspNetCore.Http;

namespace DataTransfer.Business.Services.Concrete
{
    public class GroupCodeService : RepositoryItm<GroupCode>, IGroupCodeService
    {
        public GroupCodeService(ItmDbContext context, IHttpContextAccessor httpContextAccessor) : base(context, httpContextAccessor)
        {
        }
    }
}