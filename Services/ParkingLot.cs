using ParkingLotSystem.Models;
using ParkingLotSystem.Utils;

namespace ParkingLotSystem.Services
{
    public class ParkingLot
    {
        private static ParkingLot _instance;
        private readonly List<ParkingSlot> slots = new();
        private readonly Dictionary<string, Ticket> activeTickets = new();

        private ParkingLot(int totalSlots)
        {
            for (int i = 1; i <= totalSlots; i++)
                slots.Add(new ParkingSlot { SlotNumber = i });
        }

        public static ParkingLot GetInstance(int totalSlots = 10)
        {
            if (_instance == null)
                _instance = new ParkingLot(totalSlots);
            return _instance;
        }

        public Ticket ParkVehicle(Vehicle vehicle)
        {
            var slot = slots.FirstOrDefault(s => !s.IsOccupied);
            if (slot == null)
                throw new Exception("Parking Full!");

            slot.IsOccupied = true;
            slot.OccupyingVehicle = vehicle;

            var ticket = new Ticket
            {
                TicketId = Guid.NewGuid().ToString(),
                Vehicle = vehicle,
                EntryTime = DateTime.Now
            };

            activeTickets[ticket.TicketId] = ticket;
            return ticket;
        }

        public decimal UnparkVehicle(string ticketId)
        {
            if (!activeTickets.TryGetValue(ticketId, out var ticket))
                throw new Exception("Invalid Ticket ID!");

            var slot = slots.First(s => s.OccupyingVehicle?.LicensePlate == ticket.Vehicle.LicensePlate);
            slot.IsOccupied = false;
            slot.OccupyingVehicle = null;

            var duration = (DateTime.Now - ticket.EntryTime).TotalHours;
            decimal fee = FeeCalculator.CalculateFee(ticket.Vehicle.Type, duration);

            activeTickets.Remove(ticketId);
            return fee;
        }

        public void DisplayStatus()
        {
            Console.WriteLine("\n--- Parking Lot Status ---");
            foreach (var slot in slots)
            {
                string status = slot.IsOccupied ? $"Occupied by {slot.OccupyingVehicle.LicensePlate}" : "Available";
                Console.WriteLine($"Slot {slot.SlotNumber}: {status}");
            }
            Console.WriteLine();
        }
    }
}