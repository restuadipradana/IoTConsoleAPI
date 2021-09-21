namespace IoTConsoleAPI.Data.DTO
{
    public class LocationDTO
    {
        public string LocationId { get; set; }
        public string LocationName { get; set; }
        public string Remark { get; set; }
        public string Parent { get; set; }
        public bool IsActive { get; set; }
    }
}