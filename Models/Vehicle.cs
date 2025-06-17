namespace ParkingLotSystem.Models
{
    public abstract class Vehicle
    {
        public string LicensePlate { get; set; }
        public DateTime EntryTime { get; set; }
        public abstract VehicleType Type { get; }
    }
}
