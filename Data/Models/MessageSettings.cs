using System;
using System.ComponentModel.DataAnnotations;

namespace IoTConsoleAPI.Data.Models
{
    public class MessageSettings
    {
        [Key]
        public int Id { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
        public string Remark { get; set; }
    }
}