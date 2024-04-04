
using DataTransfer.Model.Entities;

namespace DataTransfer.Business.Methods.Abstract
{
    public interface IOperationMethod
    {
        List<Operation>? Get();
        Task<Operation?> Get(int id);
        Task<Operation?> Post(Operation model);
        Task<Operation?> Put(int id, Operation model);
        Task<Operation?> Delete(int id);
    }
}
