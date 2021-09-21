using System.Threading.Tasks;
using IoTConsoleAPI.Data.DTO;
using IoTConsoleAPI.Data.Models;
using System.Collections.Generic;
using IoTConsoleAPI.Helpers;


namespace IoTConsoleAPI._Services.Interfaces
{
    public interface ISettingService
    {
         Task<List<DeviceDTO>> FetchListDevices();
         Task<bool> AddDevice(DeviceDTO device);
         Task<bool> EditDevice(DeviceDTO device);
         Task<bool> DeleteDevice(DeviceDTO device);
         Task<bool> DeviceCheckExists(string deviceId);
         Task<List<LocationDTO>> FetchListLocation();
         Task<bool> AddLocation(LocationDTO location);
         Task<bool> EditLocation(LocationDTO location);
         Task<bool> DeleteLocation(LocationDTO location);
         Task<bool> LocationCheckExists(string locationId);

         Task<List<DeviceLocationView>> FetchListDeviceLocation();
         Task<DeviceAndLocation> FetchAvailableDevice_Location();
         Task<bool> AddDeviceLocation(DeviceLocationDTO dl);
         Task<bool> EditDeviceLocation(DeviceLocationDTO dl);

         Task<bool> DeviceLocationCheckExists(string id, int kind);

         Task<string> FeedbackMessage(string code);
    }
}