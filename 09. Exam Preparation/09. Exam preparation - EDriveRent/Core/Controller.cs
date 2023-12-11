using EDriveRent.Core.Contracts;
using EDriveRent.Models;
using EDriveRent.Models.Contracts;
using EDriveRent.Repositories;
using EDriveRent.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDriveRent.Core
{
    public class Controller : IController
    {
        private UserRepository users;
        private VehicleRepository vehicles;
        private RouteRepository routes;

        public Controller()
        {
            users = new UserRepository();
            vehicles = new VehicleRepository();
            routes = new RouteRepository();
        }

        public string AllowRoute(string startPoint, string endPoint, double length)
        {
            IRoute route = new Route(startPoint, endPoint, length, routes.GetAll().Count + 1);
            IRoute existingRoute = routes.GetAll().FirstOrDefault(r => r.StartPoint == startPoint && r.EndPoint == endPoint && r.Length == length);
            IRoute shortRoute = routes.GetAll().FirstOrDefault(r => r.StartPoint == startPoint && r.EndPoint == endPoint && r.Length < length);

            if (existingRoute != null)
            {
                return String.Format(OutputMessages.RouteExisting, startPoint, endPoint, length);
            }
            else if (shortRoute != null)
            {
                return String.Format(OutputMessages.RouteIsTooLong, startPoint, endPoint);
            }
            else
            {
                IRoute newRoute = new Route(startPoint, endPoint, length, routes.GetAll().Count + 1);
                routes.AddModel(newRoute);
            }
            IRoute biggerRoute = routes.GetAll().FirstOrDefault(r => r.StartPoint == startPoint && r.EndPoint == endPoint && r.Length > length);
            if (biggerRoute != null)
            {
                biggerRoute.LockRoute();
            }

            return String.Format(OutputMessages.NewRouteAdded, startPoint, endPoint, length);
        }

        public string MakeTrip(string drivingLicenseNumber, string licensePlateNumber, string routeId, bool isAccidentHappened)
        {
            IUser user = users.GetAll().First(u => u.DrivingLicenseNumber == drivingLicenseNumber);
            IVehicle vehicle = vehicles.GetAll().First(v => v.LicensePlateNumber == licensePlateNumber);
            IRoute route = routes.GetAll().First(r => r.RouteId == int.Parse(routeId));
            if (user.IsBlocked == true)
            {
                return String.Format(OutputMessages.UserBlocked, drivingLicenseNumber);
            }
            if (vehicle.IsDamaged == true)
            {
                return String.Format(OutputMessages.VehicleDamaged, licensePlateNumber);
            }
            if (route.IsLocked == true)
            {
                return String.Format(OutputMessages.RouteLocked, routeId);
            }

            vehicle.Drive(route.Length);

            if (isAccidentHappened == true)
            {
                if (vehicle.IsDamaged == false)
                {
                    vehicle.ChangeStatus();
                    user.DecreaseRating();
                }

            }
            else
            {
                user.IncreaseRating();
            }

            return vehicle.ToString();
        }

        public string RegisterUser(string firstName, string lastName, string drivingLicenseNumber)
        {
            IUser user = users.FindById(drivingLicenseNumber);
            if (user != null)
            {
                return String.Format(OutputMessages.UserWithSameLicenseAlreadyAdded, drivingLicenseNumber);
            }
            user = new User(firstName, lastName, drivingLicenseNumber);
            users.AddModel(user);
            return $"{String.Format(OutputMessages.UserSuccessfullyAdded, firstName, lastName, drivingLicenseNumber)}";
        }

        public string RepairVehicles(int count)
        {
            List<IVehicle> brokenVehicles = new List<IVehicle>();
            foreach (var vehicle in vehicles.GetAll())
            {
                if (vehicle.IsDamaged == true)
                {
                    brokenVehicles.Add(vehicle);
                }
            }
            brokenVehicles = brokenVehicles.OrderBy(v => v.Brand).ThenBy(v => v.Model).Take(count).ToList();

            foreach (var vehicle in brokenVehicles)
            {
                vehicle.ChangeStatus();
                vehicle.Recharge();
            }
            return String.Format(OutputMessages.RepairedVehicles, brokenVehicles.Count);
        }

        public string UploadVehicle(string vehicleType, string brand, string model, string licensePlateNumber)
        {
            if (vehicleType != typeof(CargoVan).Name && vehicleType != typeof(PassengerCar).Name)
            {
                return $"{String.Format(OutputMessages.VehicleTypeNotAccessible, vehicleType)}";
            }

            IVehicle vehicle = vehicles.FindById(licensePlateNumber);
            if (vehicle != null)
            {
                return String.Format(OutputMessages.LicensePlateExists, licensePlateNumber);
            }

            if (vehicleType == typeof(CargoVan).Name)
            {
                vehicle = new CargoVan(brand, model, licensePlateNumber);
            }
            else if (vehicleType == typeof(PassengerCar).Name)
            {
                vehicle = new PassengerCar(brand, model, licensePlateNumber);
            }
            vehicles.AddModel(vehicle);
            return String.Format(OutputMessages.VehicleAddedSuccessfully, brand, model, licensePlateNumber);
        }

        public string UsersReport()
        {
            StringBuilder sb = new();
            List<IUser> customUsers = users.GetAll().OrderByDescending(u => u.Rating)
                 .ThenBy(u => u.FirstName)
                 .ToList();
            sb.AppendLine("*** E-Drive-Rent ***");
            foreach (var user in customUsers)
            {
                sb.AppendLine($"{user.ToString()}");
            }
            return sb.ToString().Trim();
        }
    }
}
