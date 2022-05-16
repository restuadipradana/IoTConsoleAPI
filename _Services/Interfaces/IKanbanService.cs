using System.Threading.Tasks;
using IoTConsoleAPI.Data.DTO;
using IoTConsoleAPI.Data.Models;
using System.Collections.Generic;
using IoTConsoleAPI.Helpers;

namespace IoTConsoleAPI._Services.Interfaces
{
    public interface IKanbanService
    {
        Task<List<TemperatureDataDTO>> TestService() ;
        Task<List<KanbanData>> FetchKanbanTemperature();
        Task<bool> AddAckDate(string location_id, string ack_date);
        Task<KanbanData> GetSingleDataLocation(string id);
    }
}