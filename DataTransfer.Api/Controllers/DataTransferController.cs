﻿using DataTransfer.Api.ADO.NET;
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
                string date_ = date.ToString("yyyy-MM-dd");
                var employeeIds = Db.EmployeeIds(date_);
                foreach (var employeeId in employeeIds)
                {
                    var entities = Db.OperatorPerformances(date_, employeeId);
                    operatorPerformances.AddRange(entities);
                }
            }
            await dataTransferMethod.DataTransfer(operatorPerformances);
            return operatorPerformances == null ? BadRequest() : Ok(operatorPerformances);
        }
        [HttpGet("StyleOperation")]
        public async Task<IActionResult> GetStyleOperations(string idOrName)
        {
            var entities = Db.StyleOperations(idOrName);
            await dataTransferMethod.StyleOperations(entities);
            return entities == null ? BadRequest() : Ok(entities);
        }
    }
}
