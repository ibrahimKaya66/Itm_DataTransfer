using DataTransfer.Model.Ado.Net;

namespace DataTransfer.Business.Methods.Abstract
{
    public interface IDataTransferMethod
    {
        Task DataTransfer(List<OperatorPerformance> models);
        Task StyleOperations(List<StyleOperation> models);
    }
}
