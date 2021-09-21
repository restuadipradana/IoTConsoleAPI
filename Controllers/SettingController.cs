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
    public class SettingController : ControllerBase
    {
        private readonly ISettingService _settingService;
        public SettingController(ISettingService settingService)
        {
            _settingService = settingService;
        }

        [HttpGet("get-device")]
        public async Task<IActionResult> GetDevice()
        {
            var lists = await _settingService.FetchListDevices();
            return Ok(lists);
        }

        [HttpPost("add-device")]
        public async Task<IActionResult> AddDevice(DeviceDTO device)
        {
            if (await _settingService.DeviceCheckExists(device.DeviceId))
            {
                return BadRequest(await _settingService.FeedbackMessage("DvcEx"));
            }
            
            if (await _settingService.AddDevice(device))
            {
                return NoContent();
            }

            throw new Exception("Creating the Device failed on save");
            
        }

        [HttpPost("edit-device")]
        public async Task<IActionResult> EditDevice(DeviceDTO device)
        {
            if (await _settingService.EditDevice(device))
            {
                return NoContent();
            }
            
            throw new Exception("Editing the Device failed on save");
        }

        [HttpPost("delete-device")]
        public async Task<IActionResult> DeleteDevice(DeviceDTO device)
        {
            if (await _settingService.DeviceLocationCheckExists(device.DeviceId, 1))
            {
                return BadRequest(await _settingService.FeedbackMessage("DvcExLoc"));
            }
            if (await _settingService.DeleteDevice(device))
            {
                return NoContent();
            }
            
            throw new Exception("Deleting the Device failed on save");
        }


        [HttpGet("get-location")]
        public async Task<IActionResult> GetLocation()
        {
            var lists = await _settingService.FetchListLocation();
            return Ok(lists);
        }

        [HttpPost("add-location")]
        public async Task<IActionResult> AddLocation(LocationDTO location)
        {
            if (await _settingService.LocationCheckExists(location.LocationId))
            {
                return BadRequest(await _settingService.FeedbackMessage("LocEx"));
            }
            
            if (await _settingService.AddLocation(location))
            {
                return NoContent();
            }

            throw new Exception("Creating the Location failed on save");
            
        }

        [HttpPost("edit-location")]
        public async Task<IActionResult> EditLocation(LocationDTO location)
        {
            if (await _settingService.EditLocation(location))
            {
                return NoContent();
            }
            
            throw new Exception("Editing the Location failed on save");
        }

        [HttpPost("delete-location")]
        public async Task<IActionResult> DeleteLocatione(LocationDTO location)
        {
            if (await _settingService.DeviceLocationCheckExists(location.LocationId, 2))
            {
                return BadRequest(await _settingService.FeedbackMessage("LocExDvc"));
            }
            if (await _settingService.DeleteLocation(location))
            {
                return NoContent();
            }
            
            throw new Exception("Deleting the Location failed on save");
        }

        [HttpGet("get-devicelocation")]
        public async Task<IActionResult> GetDeviceLocation()
        {
            var lists = await _settingService.FetchListDeviceLocation();
            return Ok(lists);
        }

        [HttpGet("get-available-devicelocation")]
        public async Task<IActionResult> GetDeviceAndLocation()
        {
            var lists = await _settingService.FetchAvailableDevice_Location();
            return Ok(lists);
        }

        [HttpPost("add-devicelocation")]
        public async Task<IActionResult> AddDeviceLocation(DeviceLocationDTO dl)
        {            
            if (await _settingService.AddDeviceLocation(dl))
            {
                return NoContent();
            }

            throw new Exception("Creating the Device Location failed on save");
            
        }

        [HttpPost("edit-devicelocation")]
        public async Task<IActionResult> EditDeviceLocation(DeviceLocationDTO dl)
        {
            if (await _settingService.EditDeviceLocation(dl))
            {
                return NoContent();
            }
            
            throw new Exception("Editing the Device Location  failed on save");
        }
    }
}