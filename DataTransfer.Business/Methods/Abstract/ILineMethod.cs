
using DataTransfer.Model.Entities;

namespace DataTransfer.Business.Methods.Abstract
{
    public interface ILineMethod
    {
        List<Line>? Get();
        Task<Line?> Get(int id);
        Task<Line?> Post(Line model);
        Task<Line?> Put(int id, Line model);
        Task<Line?> Delete(int id);
    }
}
