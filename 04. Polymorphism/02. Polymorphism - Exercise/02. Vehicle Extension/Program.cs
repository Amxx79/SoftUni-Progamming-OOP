using Vehicle.Core.Interfaces;
using Vehicle.IO.Interfaces;
using Vehicle.IO;
using Vehicle.Core;
using Vehicle.Factories;
using Vehicle.Models.Interfaces;
using Vehicle.Factories.Interfaces;

IReader reader = new ConsoleReader();
IWriter writer = new ConsoleWriter();
IVehicleFactory vehicleFactory = new VehicleFactory();

IEngine engine = new Engine(reader, writer, vehicleFactory);

engine.Run();