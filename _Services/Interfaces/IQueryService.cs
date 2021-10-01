using System.Threading.Tasks;
using IoTConsoleAPI.Data.DTO;
using IoTConsoleAPI.Data.Models;
using System.Collections.Generic;
using IoTConsoleAPI.Helpers;

namespace IoTConsoleAPI._Services.Interfaces
{
    public interface IQueryService
    {
         Task<List<TemperatureDataDTO>> SearchTemperatureData(DateRange range, string locationId);
    }
}