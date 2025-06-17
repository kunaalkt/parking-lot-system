namespace ParkingLotSystem.Models
{
    public class Ticket
    {
        public string TicketId { get; set; }
        public Vehicle Vehicle { get; set; }
        public DateTime EntryTime { get; set; }
    }
}