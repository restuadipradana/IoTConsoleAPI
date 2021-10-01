using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

using IoTConsoleAPI._Services.Services;
using IoTConsoleAPI._Services.Interfaces;
using IoTConsoleAPI.Data;
using IoTConsoleAPI.Data.Models;
using IoTConsoleAPI.Data.DTO;
using IoTConsoleAPI.Helpers;

namespace IoTConsoleAPI._Services.Services
{
    public class QueryService : IQueryService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configMapper;
        public QueryService(DataContext context, IMapper mapper, MapperConfiguration configMapper)
        {
            _context = context;
            _mapper = mapper;
            _configMapper = configMapper;
        }

        public async Task<List<TemperatureDataDTO>> SearchTemperatureData(DateRange range, string locationId) 
        {
            var deviceLocation = _context.DeviceLocation.Where(x => x.IsActive == true).AsEnumerable();
            var jsdh = range.StartDate.ToLocalTime();
            var dhus = range.EndDate.ToLocalTime();
            var locName = _context.Location.AsQueryable().ToList();
            var data =  await _context.TemperatureData.Where(w => w.InsertAt >= range.StartDate.ToLocalTime() && w.InsertAt <= range.EndDate.ToLocalTime())
                .Select(x => new TemperatureDataDTO {
                    Id = x.Id,
                    LocationId = x.LocationId,
                    DeviceId = x.DeviceId,
                    LocationName = _context.Location.Where(y => y.LocationId == x.LocationId).Select(x => x.LocationName).SingleOrDefault(),
                    Gateway = x.Gateway,
                    Temperature = x.Temperature,
                    Humidity = x.Humidity,
                    Altitude = x.Altitude,
                    Pressure = x.Pressure,
                    InsertAt = x.InsertAt,
                    DetectAt = x.DetectAt
                }).OrderBy(o => o.InsertAt)
                .ToListAsync();
            if (!string.IsNullOrEmpty(locationId))
            {
                data = data.Where(x => x.LocationId == locationId).ToList();
            }
            return data;
        } 
    }
}