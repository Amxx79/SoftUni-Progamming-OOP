using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicle.Models.Interfaces;
using Vehicle.Models;


namespace Vehicle.Models
{
    public class Bus : Vehicle
    {
        private const double increaseConsmpt = 0;
        public Bus(double fuelQuantity, double fuelConsumption, int tankCapacity)
            : base(fuelQuantity, fuelConsumption, tankCapacity)
        {
        }

        public override string Drive(double distance, bool isIncreased = true)
        {
            double consumption;
            if (isIncreased)
            {
                consumption = FuelConsumption + 1.4;
            }
            else
            {
                consumption = FuelConsumption;
            }

            if (FuelQuantity < consumption * distance)
            {
                throw new ArgumentException($"{this.GetType().Name} needs refueling");
            }

            FuelQuantity -= consumption * distance;
            return $"{this.GetType().Name} travelled {distance} km";
        }
    }
}
