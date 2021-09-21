using System;
using System.ComponentModel.DataAnnotations;

namespace IoTConsoleAPI.Data.Models
{
    public class Device
    {
        [Key]
        public string DeviceId { get; set; }
        public string DeviceSpec { get; set; }
        public string Remark { get; set; }
        public bool IsActive { get; set; }
    }
}