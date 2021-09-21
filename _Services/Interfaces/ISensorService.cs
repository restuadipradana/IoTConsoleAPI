using System.Threading.Tasks;
using IoTConsoleAPI.Data.DTO;
using IoTConsoleAPI.Data.Models;
using System.Collections.Generic;
using IoTConsoleAPI.Helpers;

namespace IoTConsoleAPI._Services.Interfaces
{
    public interface ISensorService
    {
        Task<List<BME280dataDTO>> GetAllDataBME();
        Task<List<BME280dataDTO>> GetRangeDataBME(DateRange range);
        Task<List<BME280dataDTO>> GetTodayDataBME();
    }
}