using System;
using System.ComponentModel.DataAnnotations;

namespace IoTConsoleAPI.Data.Models
{
    public class DeviceLocation
    {
        [Key]
        public int Id { get; set; }
        public int Sequence { get; set; }
        public string DeviceId { get; set; }
        public string LocationId { get; set; }
        public bool IsActive { get; set; }
    }
}