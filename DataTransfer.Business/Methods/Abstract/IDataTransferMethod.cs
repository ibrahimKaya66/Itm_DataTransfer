using DataTransfer.Model.Ado.Net;

namespace DataTransfer.Business.Methods.Abstract
{
    public interface IDataTransferMethod
    {
        Task DataTransfer(List<OperatorPerformance> model);
    }
}
