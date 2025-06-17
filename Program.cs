using ParkingLotSystem.Models;
using ParkingLotSystem.Services;

class Program
{
    static void Main(string[] args)
    {
        var parkingLot = ParkingLot.GetInstance(5);

        do
        {
            Console.WriteLine("--- Parking Lot Menu ---");
            Console.WriteLine("Choose an option: ");
            Console.WriteLine("1. Park Vehicle");
            Console.WriteLine("2. Unpark Vehicle");
            Console.WriteLine("3. View Parking Lot Status");
            Console.WriteLine("4. Exit");
            string choice = Console.ReadLine();

            try
            {
                switch (choice)
                {
                    case "1":
                    {
                        Console.Write("Enter vehicle type (Car/Bike): ");
                        string type = Console.ReadLine();
                        Console.Write("Enter license plate: ");
                        string plate = Console.ReadLine();
                        if (string.IsNullOrEmpty(plate)) 
                        {
                            Console.WriteLine("Invalid Plate Number!");
                            return;
                        }

                        Vehicle vehicle = type.ToLower() == "car" ? new Car() : new Bike();
                        vehicle.LicensePlate = plate;
                        vehicle.EntryTime = DateTime.Now;

                        var ticket = parkingLot.ParkVehicle(vehicle);
                        Console.WriteLine($"Vehicle parked. Ticket ID: {ticket.TicketId}\n");
                        break;
                    }

                    case "2":
                    {
                        Console.Write("Enter Ticket ID: ");
                        string ticketId = Console.ReadLine();
                        var fee = parkingLot.UnparkVehicle(ticketId);
                        Console.WriteLine($"Vehicle unparked. Fee: ₹{fee}\n");
                        break;
                    }

                    case "3":
                    {
                        parkingLot.DisplayStatus();
                        break;
                    }
                        
                    case "4":
                    { 
                        Console.WriteLine("Exiting...\n");
                        return;
                    }

                    default:
                    {
                        Console.WriteLine("Invalid choice.\n");
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}\n");
            }
        } while (true);
    }
}