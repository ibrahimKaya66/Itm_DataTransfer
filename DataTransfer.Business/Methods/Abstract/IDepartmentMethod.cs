using DataTransfer.Model.Entities;

namespace DataTransfer.Business.Methods.Abstract
{
    public interface IDepartmentMethod
    {
        List<Department>? Get();
        Task<Department?> Get(int id);
        Task<Department?> Post(Department model);
        Task<Department?> Put(int id, Department model);
        Task<Department?> Delete(int id);
    }
}
