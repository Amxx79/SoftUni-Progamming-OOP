using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicle.Models.Interfaces;

namespace Vehicle.Models
{
    public class Truck : Vehicle
    {
        private const double IncreasedFuel = 1.6;

        public Truck(double fuelQuantity, double fuelConsumption, int tankCapacity) 
            : base(fuelQuantity, fuelConsumption, tankCapacity)
        {
        }

        public override void Refuel(double amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Fuel must be a positive number");
            }
            else if (amount + FuelQuantity > TankCapacity)
            {
                throw new ArgumentException($"Cannot fit {amount} fuel in the tank");
            }
            base.Refuel(amount * 0.95);
        }
    }
}
