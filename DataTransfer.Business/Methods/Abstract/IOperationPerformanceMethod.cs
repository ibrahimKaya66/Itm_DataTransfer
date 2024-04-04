
using DataTransfer.Model.Entities;

namespace DataTransfer.Business.Methods.Abstract
{
    public interface IOperationPerformanceMethod
    {
        List<OperationPerformance>? Get();
        Task<OperationPerformance?> Get(int id);
        Task<OperationPerformance?> Post(OperationPerformance model);
        Task<OperationPerformance?> Put(int id, OperationPerformance model);
        Task<OperationPerformance?> Delete(int id);
    }
}
