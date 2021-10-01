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
    public class KanbanService : IKanbanService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configMapper;
        public KanbanService(DataContext context, IMapper mapper, MapperConfiguration configMapper)
        {
            _context = context;
            _mapper = mapper;
            _configMapper = configMapper;
        }

        public async Task<List<TemperatureDataDTO>> TestService() 
        {
            var locName = _context.Location.AsQueryable().ToList();
            DateTime todays = DateTime.Now.Date;
            DateTime todaye = DateTime.Now.Date.AddHours(23).AddMinutes(59).AddSeconds(59);
            var data =  await _context.TemperatureData.Where(w => w.InsertAt >= todays && w.InsertAt <= todaye)
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
                }).OrderByDescending(o => o.InsertAt)
                .ToListAsync();

            var src = data.Select(m => new 
                        {
                            Key = new
                            {
                                m.LocationId
                            },
                            Message = m
                        });
            
            var finalData = src.Select(e => e.Key).Distinct()
                .SelectMany(key => src
                    .Where(e => e.Key.LocationId == key.LocationId)
                    .Select(e => e.Message)
                    .Take(1)).ToList(); 
            return finalData;
        }
        
        
        /* -- ================ Runnning code start here =================== -- */
        public async Task<List<KanbanData>> FetchKanbanTemperature()
        {
            var deviceLocation = _context.DeviceLocation.Where(x => x.IsActive == true).AsEnumerable();
            var locName = _context.Location.AsQueryable().ToList();
            DateTime todays = DateTime.Now.Date;
            DateTime todaye = DateTime.Now.Date.AddHours(23).AddMinutes(59).AddSeconds(59);
            var data =  await _context.TemperatureData.Where(w => w.InsertAt >= todays && w.InsertAt <= todaye)
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
                }).OrderByDescending(o => o.InsertAt)
                .ToListAsync();

            // today data group by loc id
            var src = data.Select(m => new 
                        {
                            Key = new
                            {
                                m.LocationId
                            },
                            Message = m
                        });
            
            //show first data per group
            var finalData = src.Select(e => e.Key).Distinct()
                .SelectMany(key => src
                    .Where(e => e.Key.LocationId == key.LocationId)
                    .Select(e => e.Message)
                    .Take(1)).ToList(); 

            // inner join with active device location (just show data with active DeviceLocation)
            var exactlyFinalKanban = (from d in deviceLocation
                                        join c in finalData on d.LocationId equals c.LocationId
                                        orderby d.Sequence
                                        select new KanbanData()
                                        {
                                            Sequence = d.Sequence,
                                            LocationName = c.LocationName,
                                            TemperatureDataId = c.Id,
                                            Temperature = c.Temperature,
                                            Humidity = c.Humidity,
                                            LastUpdate = c.InsertAt
                                        }).ToList();
            return exactlyFinalKanban;
        } 
    }
}