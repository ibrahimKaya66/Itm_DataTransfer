using ItmProject.Model.DTOs;

namespace DataTransfer.Business.Methods.Abstract
{
    public interface IOperationMethod
    {
        List<OperationDTO>? Get();
        Task<OperationDTO?> Get(int id);
        Task<OperationDTO?> Post(OperationDTO model);
        Task<OperationDTO?> Put(int id, OperationDTO model);
        Task<OperationDTO?> Delete(int id);
    }
}
