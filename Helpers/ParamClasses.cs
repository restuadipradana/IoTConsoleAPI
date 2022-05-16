using System;
using IoTConsoleAPI.Data.Models;

namespace IoTConsoleAPI.Helpers
{
    public class DateRange
    {
        public DateTime StartDate {get; set;}
        public DateTime EndDate {get; set;}
    }

    public class KanbanData //store kanban data
    {
        public int Sequence {get; set;}
        public int TemperatureDataId {get; set;}
        public string LocationId {get; set;}
        public string LocationName {get; set;}
        public double Temperature {get; set;}
        public double Humidity {get; set;}
        public DateTime LastUpdate {get; set;}
        public double MinTemperature { get; set; }
        public double MinHumidity { get; set; }
        public double MaxTemperature { get; set; }
        public double MaxHumidity { get; set; }
        public DateTime? LastAcknowledgeDate {get; set;}
    }

    public class DeviceLocationView //maintain service purpose
    {
        public int Id {get; set;}
        public int Sequence { get; set; }
        public string DeviceId { get; set; }
        public string DeviceSpec { get; set; }
        public string LocationId { get; set; }
        public string LocationName { get; set; }
        public bool IsActive { get; set; }
        public double MinTemperature { get; set; }
        public double MinHumidity { get; set; }
        public double MaxTemperature { get; set; }
        public double MaxHumidity { get; set; }
        public DateTime LastAcknowledgeDate {get; set;}
    }

    public class DeviceAndLocation //maintain service purpose
    {
        public Device[] AvailableDevices {get; set;}
        public Location[] AvailableLocations {get; set;}
    }
}