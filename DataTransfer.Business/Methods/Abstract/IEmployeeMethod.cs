
using DataTransfer.Model.Entities;

namespace DataTransfer.Business.Methods.Abstract
{
    public interface IEmployeeMethod
    {
        List<Employee>? Get();
        Task<Employee?> Get(int id);
        Task<Employee?> Post(Employee model);
        Task<Employee?> Put(int id, Employee model);
        Task<Employee?> Delete(int id);
    }
}
