using DataTransfer.Api.ADO.NET;
using DataTransfer.Api.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DataTransfer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataTransferController : ControllerBase
    {
        private readonly AppSettings appSettings;

        public DataTransferController(IOptions<AppSettings> options)
        {
            appSettings = options.Value;
        }

        [HttpGet]
        public IActionResult GetOperatorPerformances(string? startDate = null, string? endDate = null)
        {
            var entities = Db.OperatorPerformances(startDate, endDate);

            return entities == null ? BadRequest() : Ok(entities);
        }
    }
}
