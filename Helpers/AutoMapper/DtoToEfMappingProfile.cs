using AutoMapper;
using IoTConsoleAPI.Data.DTO;
using IoTConsoleAPI.Data.Models;

namespace IoTConsoleAPI.Helpers.AutoMapper
{
    public class DtoToEfMappingProfile : Profile
    {
        public DtoToEfMappingProfile()
        {
            CreateMap<BME280dataDTO, BME280data>();
            CreateMap<SensorDetectDTO, SensorDetect>();
            CreateMap<TemperatureDataDTO, TemperatureData>();
            CreateMap<DeviceLocationDTO, DeviceLocation>();
            CreateMap<DeviceDTO, Device>();
            CreateMap<LocationDTO, Location>();
        }
    }
}