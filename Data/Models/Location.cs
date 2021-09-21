using System;
using System.ComponentModel.DataAnnotations;

namespace IoTConsoleAPI.Data.Models
{
    public class Location
    {
        [Key]
        public string LocationId { get; set; }
        public string LocationName { get; set; }
        public string Remark { get; set; }
        public string Parent { get; set; }
        public bool IsActive { get; set; }
    }
}