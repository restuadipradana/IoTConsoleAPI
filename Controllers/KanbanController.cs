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
    [ApiController]
    [Route("api/[controller]")]
    public class KanbanController : ControllerBase
    {
        private readonly IKanbanService _kanbanService;
        public KanbanController(IKanbanService kanbanService)
        {
            _kanbanService = kanbanService;
        }

        [HttpGet("getest")]
        public async Task<IActionResult> GetDataBMESens()
        {
            var lists = await _kanbanService.TestService();
            return Ok(lists);
        }

        [HttpGet("get-kanban")]
        public async Task<IActionResult> GetKanbanData()
        {
            var lists = await _kanbanService.FetchKanbanTemperature();
            return Ok(lists);
        }

        [HttpGet("set-danger-acknowledge")]
        public async Task<IActionResult> SetAckDanger(string loc_id, string ack_date)
        {
            if (await _kanbanService.AddAckDate(loc_id, ack_date))
            {
                return NoContent();
            }

            throw new Exception("Faile to set last ack date");
        }

        [HttpGet("get-single-data")]
        public async Task<IActionResult> GetSingleData(string id)
        {
            var data = await _kanbanService.GetSingleDataLocation(id);
            return Ok(data);
        }
    }
}