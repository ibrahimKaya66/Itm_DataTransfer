using ItmProject.Model.DTOs;

namespace DataTransfer.Business.Methods.Abstract
{
    public interface IGroupMethod
    {
        List<GroupDTO>? Get();
        Task<GroupDTO?> Get(int id);
        Task<GroupDTO?> Post(GroupDTO model);
        Task<GroupDTO?> Put(int id, GroupDTO model);
        Task<GroupDTO?> Delete(int id);
        List<GroupDTO>? GetWithGroupCode(int? groupCodeId);
    }
}
