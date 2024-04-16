using DataTransfer.Api.ADO.NET;
using DataTransfer.Api.Model;
using DataTransfer.Business.Methods.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DataTransfer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataTransferController : ControllerBase
    {
        private readonly AppSettings appSettings;
        private readonly IDataTransferMethod dataTransferMethod;

        public DataTransferController(IOptions<AppSettings> options, IDataTransferMethod dataTransferMethod)
        {
            appSettings = options.Value;
            this.dataTransferMethod = dataTransferMethod;
        }

        [HttpGet]
        public async Task<IActionResult> GetOperatorPerformances(string? startDate = null, string? endDate = null)
        {
            var entities = Db.OperatorPerformances(startDate, endDate);
            await dataTransferMethod.DataTransfer(entities);
            return entities == null ? BadRequest() : Ok(entities);
        }
        [HttpGet]
        public async Task<IActionResult> GetStyleOperations(string styleName)
        {
            var entities = Db.StyleOperations(styleName);
            await dataTransferMethod.StyleOperations(styleName,entities);
            return entities == null ? BadRequest() : Ok(entities);
        }
    }
}
