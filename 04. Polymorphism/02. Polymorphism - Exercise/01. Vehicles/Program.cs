using Vehicle.Core.Interfaces;
using Vehicle.IO.Interfaces;
using Vehicle.IO;
using Vehicle.Core;
using Vehicle.Factories;

IReader reader = new ConsoleReader();
IWriter writer = new ConsoleWriter();

IEngine engine = new Engine(reader, writer);

engine.Run();