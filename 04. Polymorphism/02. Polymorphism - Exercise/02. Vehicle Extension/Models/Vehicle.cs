using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Vehicle.Models.Interfaces;

namespace Vehicle.Models
{
    public abstract class Vehicle : IVehicle
    {
        private double fuelQuantity;
        private double increasedConsumption;

        protected Vehicle(double fuelQuantity, double fuelConsumption, int tankCapacity)
        {
            FuelConsumption = fuelConsumption;
            TankCapacity = tankCapacity;
            FuelQuantity = fuelQuantity;
        }

        public double FuelQuantity
        {
            get
            {
                return fuelQuantity;
            }
            set
            {
                if (value > TankCapacity)
                {
                    fuelQuantity = 0;
                    return;
                }
                fuelQuantity = value;
            }
        }
        public double FuelConsumption { get; set; }
        public int TankCapacity { get; set; }

        public virtual string Drive(double distance, bool isIncreased = true)
        {
            double consumption = FuelConsumption + increasedConsumption;

            if (FuelQuantity < consumption * distance)
            {
                throw new ArgumentException($"{this.GetType().Name} needs refueling");
            }

            FuelQuantity -= consumption * distance;
            return $"{this.GetType().Name} travelled {distance} km";
        }

        public virtual void Refuel(double amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Fuel must be a positive number");
            }
            else if (amount > TankCapacity)
            {
                throw new ArgumentException($"Cannot fit {amount} fuel in the tank");
            }
            FuelQuantity += amount;
        }
        public override string ToString()
        {
            return $"{this.GetType().Name}: {FuelQuantity:F2}";
        }
    }
}
