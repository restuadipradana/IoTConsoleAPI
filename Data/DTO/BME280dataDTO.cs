using System;

namespace IoTConsoleAPI.Data.DTO
{
    public class BME280dataDTO
    {
        public int Id { get; set; }
        public string Location { get; set; }
        public string Gateway { get; set; }
        public double? Temperature { get; set; }
        public double? Humidity { get; set; }
        public double? Altitude { get; set; }
        public double? Pressure { get; set; }
        public int? TimeBucket { get; set; }
        public DateTime? DetectAt { get; set; }
        public DateTime? InsertAt { get; set; }
    }
}