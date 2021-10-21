using System;
using System.ComponentModel.DataAnnotations;

namespace IoTConsoleAPI.Data.Models
{
    /* THIS DB TABLE IS DEPRECATED*/
    public class BME280data
    {
        [Key]
        public int Id { get; set; }
        public string Location { get; set; }
        public string Gateway { get; set; }
        public double Temperature { get; set; }
        public double Humidity { get; set; }
        public double Altitude { get; set; }
        public double Pressure { get; set; }
        public int TimeBucket { get; set; }
        public DateTime DetectAt { get; set; }
        public DateTime InsertAt { get; set; }
    }
}