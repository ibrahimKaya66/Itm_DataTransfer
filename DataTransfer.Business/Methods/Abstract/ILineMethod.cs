using ItmProject.Model.DTOs;

namespace DataTransfer.Business.Methods.Abstract
{
    public interface ILineMethod
    {
        List<LineDTO>? Get();
        Task<LineDTO?> Get(int id);
        Task<LineDTO?> Post(LineDTO model);
        Task<LineDTO?> Put(int id, LineDTO model);
        Task<LineDTO?> Delete(int id);
    }
}
