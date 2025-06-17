namespace ParkingLotSystem.Models
{
    public class ParkingSlot
    {
        public int SlotNumber { get; set; }
        public bool IsOccupied { get; set; }
        public Vehicle OccupyingVehicle { get; set; }
    }
}