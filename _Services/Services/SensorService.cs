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
    public class SensorService : ISensorService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configMapper;
        public SensorService(DataContext context, IMapper mapper, MapperConfiguration configMapper)
        {
            _context = context;
            _mapper = mapper;
            _configMapper = configMapper;
        }

        public async Task<List<BME280dataDTO>> GetAllDataBME()
        {
            var data = await _context.BME280data.Select(x => new BME280dataDTO {
                Id = x.Id,
                Location = x.Location,
                Gateway = x.Gateway,
                Temperature = x.Temperature,
                Humidity = x.Humidity,
                Altitude = x.Altitude,
                Pressure = x.Pressure,
                InsertAt = x.InsertAt,
                DetectAt = x.DetectAt
            }).ToListAsync();
            return data;
        }

        public async Task<List<BME280dataDTO>> GetTodayDataBME()
        {
            DateTime todays = DateTime.Now.Date;
            DateTime todaye = DateTime.Now.Date.AddHours(23).AddMinutes(59).AddSeconds(59);
            DateTime etoday = DateTime.Today.AddHours(23).AddMinutes(59).AddSeconds(59);
            
            var data = await _context.BME280data.Where(w => w.InsertAt >= todays && w.InsertAt <= todaye).Select(x => new BME280dataDTO {
                Id = x.Id,
                Location = x.Location,
                Gateway = x.Gateway,
                Temperature = x.Temperature,
                Humidity = x.Humidity,
                Altitude = x.Altitude,
                Pressure = x.Pressure,
                InsertAt = x.InsertAt,
                DetectAt = x.DetectAt
            }).OrderBy(o => o.InsertAt).ToListAsync();
            return data;
        }

        public async Task<List<BME280dataDTO>> GetRangeDataBME(DateRange range)
        {
            var data = await _context.BME280data.Where(w => w.InsertAt >= range.StartDate.ToLocalTime() && w.InsertAt <= range.EndDate.ToLocalTime()).Select(x => new BME280dataDTO {
                Id = x.Id,
                Location = x.Location,
                Gateway = x.Gateway,
                Temperature = x.Temperature,
                Humidity = x.Humidity,
                Altitude = x.Altitude,
                Pressure = x.Pressure,
                InsertAt = x.InsertAt,
                DetectAt = x.DetectAt
            }).OrderByDescending(o => o.InsertAt).ToListAsync();
            return data;
        }

        /* -- ================ Runnning code start here =================== -- */
    }
}