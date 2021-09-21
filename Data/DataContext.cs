using Microsoft.EntityFrameworkCore;
using IoTConsoleAPI.Data.Models;

namespace IoTConsoleAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<BME280data> BME280data {get; set;}
        public DbSet<SensorDetect> SensorDetect {get; set;}
        public DbSet<TemperatureData> TemperatureData {get; set;}
        public DbSet<Device> Device {get; set;}
        public DbSet<Location> Location {get; set;}
        public DbSet<DeviceLocation> DeviceLocation {get; set;}
        public DbSet<MessageSettings> MessageSettings {get; set;}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BME280data>().HasKey(x =>  x.Id);
            modelBuilder.Entity<SensorDetect>().HasKey(x =>  x.Id);
            modelBuilder.Entity<TemperatureData>().HasKey(x =>  x.Id);
            modelBuilder.Entity<Device>().HasKey(x =>  x.DeviceId);
            modelBuilder.Entity<Location>().HasKey(x =>  x.LocationId);
            modelBuilder.Entity<DeviceLocation>().HasKey(x =>  x.Id);
            modelBuilder.Entity<MessageSettings>().HasKey(x =>  x.Id);
        }

    }
}