using AutoMapper;
using IoTConsoleAPI.Data.DTO;
using IoTConsoleAPI.Data.Models;

namespace IoTConsoleAPI.Helpers.AutoMapper
{
    public class EfToDtoMappingProfile : Profile
    {
        public EfToDtoMappingProfile()
        {
            CreateMap<BME280data, BME280dataDTO>();
            CreateMap<SensorDetect, SensorDetectDTO>();
            CreateMap<TemperatureData, TemperatureDataDTO>();
            CreateMap<DeviceLocation, DeviceLocationDTO>();
            CreateMap<Location, LocationDTO>();
            CreateMap<Device, DeviceDTO>();
        }
    }
}