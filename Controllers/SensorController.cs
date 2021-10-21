using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

using IoTConsoleAPI._Services.Interfaces;
using IoTConsoleAPI.Data.DTO;
using IoTConsoleAPI.Data.Models;
using IoTConsoleAPI.Helpers;

namespace IoTConsoleAPI.Controllers
{
    /*THIS CONTROLLER IS DEPRECATED*/

    [ApiController]
    [Route("api/[controller]")]
    public class SensorController : ControllerBase
    {
        private readonly ISensorService _sensorService;
        public SensorController(ISensorService sensorService)
        {
            _sensorService = sensorService;
        }

        [HttpGet("getsensor")]
        public async Task<IActionResult> GetDataBME()
        {
            var lists = await _sensorService.GetAllDataBME();
            return Ok(lists);
        }

        [HttpGet("get-today-temp")]
        public async Task<IActionResult> GetTodayDataBME()
        {
            var lists = await _sensorService.GetTodayDataBME();
            return Ok(lists);
        }

        [HttpPost("get-temp-range")]
        public async Task<IActionResult> GetDateBMERange(DateRange range) 
        {
            var data = await _sensorService.GetRangeDataBME(range);
            return Ok(data);
        }



        
    }
}