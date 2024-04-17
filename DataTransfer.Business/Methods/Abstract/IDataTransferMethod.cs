using DataTransfer.Model.Ado.Net;

namespace DataTransfer.Business.Methods.Abstract
{
    public interface IDataTransferMethod
    {
        Task DataTransfer(List<OperatorPerformance> models);
        Task StyleOperations(string styleName, List<StyleOperation> models);
    }
}
