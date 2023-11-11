using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicle.Models.Interfaces;

namespace Vehicle.Factories.Interfaces
{
    public interface IVehicleFactory
    {
        IVehicle Create(string type, double fuelQuantity, double fuelConsumption, int tankCapacity);
    }
}
