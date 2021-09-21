using System;

namespace IoTConsoleAPI.Data.DTO
{
    public class SensorDetectDTO
    {
        public int Id { get; set; }
        public string Cell { get; set; }
        public string Gateway { get; set; }
        public int Detect { get; set; }
        public DateTime? DetectAt { get; set; }
        public DateTime? InsertAt { get; set; }
    }
}