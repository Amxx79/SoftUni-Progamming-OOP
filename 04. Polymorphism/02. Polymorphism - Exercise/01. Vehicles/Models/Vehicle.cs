using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicle.Models.Interfaces;

namespace Vehicle.Models
{
    public abstract class Vehicle : IVehicle
    {
        private double increasedConsumption;

        protected Vehicle(double fuelQuantity, double fuelConsumption, double increasedConsumption)
        {
            FuelConsumption = fuelConsumption;
            FuelQuantity = fuelQuantity;
            this.increasedConsumption = increasedConsumption;
        }

        public double FuelQuantity { get; set; }
        public double FuelConsumption { get; set; }

        public string Drive(double distance)
        {
            double consumption = FuelConsumption + increasedConsumption;

            if (FuelQuantity < consumption * distance)
            {
                throw new ArgumentException($"{this.GetType().Name} needs refueling");
            }

            FuelQuantity -= consumption * distance;
            return $"{this.GetType().Name} travelled {distance} km";
        }

        public virtual void Refuel(double amount) => FuelQuantity += amount;

        public override string ToString()
        {
            return $"{this.GetType().Name}: {FuelQuantity:F2}";
        }
    }
}
