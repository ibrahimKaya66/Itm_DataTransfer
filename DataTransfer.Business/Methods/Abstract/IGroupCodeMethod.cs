
using DataTransfer.Model.Entities;

namespace DataTransfer.Business.Methods.Abstract
{
    public interface IGroupCodeMethod
    {
        List<GroupCode>? Get();
        Task<GroupCode?> Get(int id);
        Task<GroupCode?> Post(GroupCode model);
        Task<GroupCode?> Put(int id, GroupCode model);
        Task<GroupCode?> Delete(int id);
    }
}
