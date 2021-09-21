using System;
using System.ComponentModel.DataAnnotations;

namespace IoTConsoleAPI.Data.Models
{
    public class SensorDetect
    {
        [Key]
        public int Id { get; set; }
        public string Cell { get; set; }
        public string Gateway { get; set; }
        public int Detect { get; set; }
        public DateTime DetectAt { get; set; }
        public DateTime InsertAt { get; set; }
    }
}