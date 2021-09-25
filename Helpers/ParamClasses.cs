using System;
using IoTConsoleAPI.Data.Models;

namespace IoTConsoleAPI.Helpers
{
    public class DateRange
    {
        public DateTime StartDate {get; set;}
        public DateTime EndDate {get; set;}
    }

    public class KanbanData
    {
        public int Sequence {get; set;}
        public int TemperatureDataId {get; set;}
        public string LocationName {get; set;}
        public double Temperature {get; set;}
        public double Humidity {get; set;}
        public DateTime LastUpdate {get; set;}
    }

    public class DeviceLocationView
    {
        public int Id {get; set;}
        public int Sequence { get; set; }
        public string DeviceId { get; set; }
        public string DeviceSpec { get; set; }
        public string LocationId { get; set; }
        public string LocationName { get; set; }
        public bool IsActive { get; set; }
    }

    public class DeviceAndLocation
    {
        public Device[] AvailableDevices {get; set;}
        public Location[] AvailableLocations {get; set;}
    }
}