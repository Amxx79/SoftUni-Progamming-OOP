using EDriveRent.Models;
using EDriveRent.Models.Contracts;
using EDriveRent.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDriveRent.Repositories
{
    public class VehicleRepository : IRepository<IVehicle>
    {
        List<IVehicle> vehicles;
        public VehicleRepository()
        {
            vehicles = new List<IVehicle>();
        }
        public void AddModel(IVehicle model)
        {
            vehicles.Add(model);
        }

        public IVehicle FindById(string identifier)
        {
            IVehicle currVehicle = vehicles.FirstOrDefault(v => v.LicensePlateNumber == identifier);
            if (currVehicle == null)
            {
                return null;
            }
            else
            {
                return currVehicle;
            }
        }

        public IReadOnlyCollection<IVehicle> GetAll() => vehicles;

        public bool RemoveById(string identifier)
        {
            IVehicle currVehicle = vehicles.FirstOrDefault(v => v.LicensePlateNumber == identifier);
            return vehicles.Remove(currVehicle);
        }
    }
}
