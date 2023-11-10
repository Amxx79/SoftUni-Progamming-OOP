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

        public Truck(double fuelQuantity, double fuelConsumption) 
            : base(fuelQuantity, fuelConsumption, IncreasedFuel)
        {
        }

        public override void Refuel(double amount)
        {
            base.Refuel(amount * 0.95);
        }
    }
}
