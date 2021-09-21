namespace IoTConsoleAPI.Data.DTO
{
    public class DeviceLocationDTO
    {
        public int Id { get; set; }
        public int Sequence { get; set; }
        public string DeviceId { get; set; }
        public string LocationId { get; set; }
        public bool IsActive { get; set; }
    }
}