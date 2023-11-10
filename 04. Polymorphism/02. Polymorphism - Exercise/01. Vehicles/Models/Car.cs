using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicle.Models.Interfaces;

namespace Vehicle.Models
{
    public class Car : Vehicle
    {
        private const double IncreasedFuel = 0.9;
        public Car(double fuelQuantity, double fuelConsumption) 
            : base(fuelQuantity, fuelConsumption, IncreasedFuel)
        {
        }
    }
}
