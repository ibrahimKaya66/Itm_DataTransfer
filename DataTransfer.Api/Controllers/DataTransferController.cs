using DataTransfer.Api.ADO.NET;
using DataTransfer.Api.Model;
using DataTransfer.Business.Methods.Abstract;
using DataTransfer.Model.Ado.Net;
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

        [HttpGet("OperatorPerformance")]
        public async Task<IActionResult> GetOperatorPerformances(string? startDate = null, string? endDate = null)
        {
            List<OperatorPerformance> operatorPerformances = new List<OperatorPerformance>();
           
            DateTime startDateTime, endDateTime;
            if (!DateTime.TryParse(startDate, out startDateTime) || !DateTime.TryParse(endDate, out endDateTime))
            {
                return BadRequest("Geçersiz tarih formatı");
            }

            for (DateTime date = startDateTime; date <= endDateTime; date = date.AddDays(1))
            {
                var employeeIds = Db.EmployeeIds(date.ToString("dd-MM-yyyy"));
                foreach (var item in employeeIds)
                {
                    var entities = Db.OperatorPerformances();
                }
            }
            return Ok();
            //await dataTransferMethod.DataTransfer(entities);
            //return entities == null ? BadRequest() : Ok(entities);
        }
        [HttpGet("StyleOperation")]
        public async Task<IActionResult> GetStyleOperations(string styleName)
        {
            var entities = Db.StyleOperations(styleName);
            await dataTransferMethod.StyleOperations(styleName,entities);
            return entities == null ? BadRequest() : Ok(entities);
        }
    }
}
