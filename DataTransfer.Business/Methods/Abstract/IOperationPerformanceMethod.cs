using ItmProject.Model.DTOs;

namespace DataTransfer.Business.Methods.Abstract
{
    public interface IOperationPerformanceMethod
    {
        List<OperationPerformanceDTO>? Get();
        Task<OperationPerformanceDTO?> Get(int id);
        Task<OperationPerformanceDTO?> Post(OperationPerformanceDTO model);
        Task<OperationPerformanceDTO?> Put(int id, OperationPerformanceDTO model);
        Task<OperationPerformanceDTO?> Delete(int id);
    }
}
