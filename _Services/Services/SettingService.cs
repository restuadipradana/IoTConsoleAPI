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
    public class SettingService : ISettingService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configMapper;
        public SettingService(DataContext context, IMapper mapper, MapperConfiguration configMapper)
        {
            _context = context;
            _mapper = mapper;
            _configMapper = configMapper;
        }

        public async Task<List<DeviceDTO>> FetchListDevices()
        {
            var data =  _context.Device.AsQueryable();
            return await data.ProjectTo<DeviceDTO>(_configMapper).ToListAsync();
        }

        public async Task<bool> AddDevice(DeviceDTO device)
        {
            var newData = _mapper.Map<Device>(device);
            newData.IsActive = false;
            _context.Device.Add(newData);
            await _context.SaveChangesAsync();
            return true;
            
        }

        public async Task<bool> EditDevice(DeviceDTO device)
        {
            var editData = _mapper.Map<Device>(device);
            _context.Device.Update(editData);
            await _context.SaveChangesAsync();
            return true;
        }
        
        public async Task<bool> DeleteDevice(DeviceDTO device)
        {
            var deleteData = _mapper.Map<Device>(device);
            _context.Device.Remove(deleteData);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeviceCheckExists(string deviceId)
        {
            if (await _context.Device.AnyAsync(x => x.DeviceId == deviceId))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

//=====================================================================================================

        public async Task<List<LocationDTO>> FetchListLocation()
        {
            var data =  _context.Location.AsQueryable();
            return await data.ProjectTo<LocationDTO>(_configMapper).ToListAsync();
        }

        public async Task<bool> AddLocation(LocationDTO location)
        {
            var newData = _mapper.Map<Location>(location);
            newData.IsActive = false;
            _context.Location.Add(newData);
            await _context.SaveChangesAsync();
            return true;
            
        }

        public async Task<bool> EditLocation(LocationDTO location)
        {
            var editData = _mapper.Map<Location>(location);
            _context.Location.Update(editData);
            await _context.SaveChangesAsync();
            return true;
        }
        
        public async Task<bool> DeleteLocation(LocationDTO location)
        {
            var deleteData = _mapper.Map<Location>(location);
            _context.Location.Remove(deleteData);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> LocationCheckExists(string locationId)
        {
            if (await _context.Location.AnyAsync(x => x.LocationId == locationId))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

//=========================================================================================

        public async Task<List<DeviceLocationView>> FetchListDeviceLocation()
        {
            var deviceLocations =  _context.DeviceLocation.AsQueryable();
            var devices = _context.Device.AsQueryable();
            var locations = _context.Location.AsQueryable();
            var data =  await (from a in deviceLocations
                        join c in devices on a.DeviceId equals c.DeviceId
                        join d in locations on a.LocationId equals d.LocationId
                        select new DeviceLocationView()
                        {
                            Id = a.Id,
                            Sequence = a.Sequence,
                            DeviceId = a.DeviceId,
                            LocationId = a.LocationId,
                            DeviceSpec = c.DeviceSpec,
                            LocationName = d.LocationName,
                            IsActive = a.IsActive
                            
                        }).ToListAsync();

            return data;
        }

        //get decice and location yang belom ada di table Devicelocation
        public async Task<DeviceAndLocation> FetchAvailableDevice_Location()
        {
            DeviceAndLocation dnl = new DeviceAndLocation();
            dnl.AvailableDevices = await _context.Device.Where(x => x.IsActive == false).ToArrayAsync();
            dnl.AvailableLocations = await _context.Location.Where(x => x.IsActive == false).ToArrayAsync();
            return dnl;
        }

        // add deviceLocation akan merubah tabel device dan location IsActive nya jadi true
        public async Task<bool> AddDeviceLocation(DeviceLocationDTO dl)
        {
            var newData = _mapper.Map<DeviceLocation>(dl);
            _context.DeviceLocation.Add(newData);
            var dvcData =  _context.Device.Where(x => x.DeviceId == newData.DeviceId).AsQueryable().Single();
            dvcData.IsActive = true;
            _context.Device.Update(dvcData);
            var locData =  _context.Location.Where(x => x.LocationId == newData.LocationId).AsQueryable().Single();
            locData.IsActive = true;
            _context.Location.Update(locData);

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EditDeviceLocation(DeviceLocationDTO dl)
        {
            var exist = await _context.DeviceLocation.AnyAsync(x => x.Id == dl.Id);
            if (!exist) {
                throw new Exception("ProductNotExist");
            }
            
            var editData = _mapper.Map<DeviceLocation>(dl);
            var oldData = _context.DeviceLocation.AsNoTracking().Where(x => x.Id == dl.Id).Single(); //  .Find(editData.Id);
            if (oldData.Sequence != editData.Sequence)
            {
                if (await _context.DeviceLocation.AnyAsync(x => x.Sequence == dl.Sequence))
                {
                    return false;
                }
            }
            var dvcData =  _context.Device.Where(x => x.DeviceId == oldData.DeviceId).AsQueryable().Single();
            dvcData.IsActive = false;
            _context.Device.Update(dvcData);
            var locData =  _context.Location.Where(x => x.LocationId == oldData.LocationId).AsQueryable().Single();
            locData.IsActive = false;
            _context.Location.Update(locData);
            dvcData =  _context.Device.Where(x => x.DeviceId == editData.DeviceId).AsQueryable().Single();
            dvcData.IsActive = true;
            _context.Device.Update(dvcData);
            locData =  _context.Location.Where(x => x.LocationId == editData.LocationId).AsQueryable().Single();
            locData.IsActive = true;
            _context.Location.Update(locData);                
            _context.DeviceLocation.Update(editData);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteDeviceLocation(DeviceLocationDTO dl)
        {
            var delData = _mapper.Map<DeviceLocation>(dl);
            _context.DeviceLocation.Remove(delData);
            var dvcData =  _context.Device.Where(x => x.DeviceId == delData.DeviceId).AsQueryable().Single();
            dvcData.IsActive = false;
            _context.Device.Update(dvcData);
            var locData =  _context.Location.Where(x => x.LocationId == delData.LocationId).AsQueryable().Single();
            locData.IsActive = false;
            _context.Location.Update(locData);

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> SequenceCheckExists(int sequence)
        {
            if (await _context.DeviceLocation.AnyAsync(x => x.Sequence == sequence))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        //kind 1 = device, 2 = location (update : Device / Location yang isActive nya true = ada di tabel DeviceLocation)
        public async Task<bool> DeviceLocationCheckExists(string id, int kind)
        {
            if (kind == 1) 
            {
                if (await _context.DeviceLocation.AnyAsync(x => x.DeviceId == id))
                {
                    return true;
                }
                else
                {
                    return false; //gabisa delete karna device masih kepake
                }
            }
            else 
            {
                if (await _context.DeviceLocation.AnyAsync(x => x.LocationId == id))
                {
                    return true;
                }
                else
                {
                    return false; //gabisa delete karna location masih aktif
                }
            }

            
        }

        public async Task<string> FeedbackMessage(string code)
        {
            if (await _context.MessageSettings.AnyAsync(x => x.Code == code))
            {
                return await _context.MessageSettings.Where(x => x.Code == code).Select(y => y.Message).SingleAsync();;
            }
            else
            {
                return "Error ... !! ^.^ (Not found meesage)";
            }
        }
    }
}