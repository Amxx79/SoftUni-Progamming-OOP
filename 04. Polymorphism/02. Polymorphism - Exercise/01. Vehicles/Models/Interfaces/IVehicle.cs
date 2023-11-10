using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicle.Models.Interfaces
{
    public interface IVehicle
    {
        public double  FuelQuantity { get; }
        public double FuelConsumption { get; }

        string Drive(double distance);
        void Refuel(double amount);

    }
}
