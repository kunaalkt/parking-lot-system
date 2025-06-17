using ParkingLotSystem.Models;

namespace ParkingLotSystem.Utils
{
    public static class FeeCalculator
    {
        public static decimal CalculateFee(VehicleType type, double hours)
        {
            decimal rate = type switch
            {
                VehicleType.Car => 20m,
                VehicleType.Bike => 10m,
                _ => 15m
            };

            return Math.Max(1, (decimal)Math.Ceiling(hours)) * rate;
        }
    }
}