
using DataTransfer.Model.Entities;

namespace DataTransfer.Business.Methods.Abstract
{
    public interface IGroupMethod
    {
        List<Group>? Get();
        Task<Group?> Get(int id);
        Task<Group?> Post(Group model);
        Task<Group?> Put(int id, Group model);
        Task<Group?> Delete(int id);
        List<Group>? GetWithGroupCode(int? groupCodeId);
    }
}
