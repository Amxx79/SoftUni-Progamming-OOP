﻿using Vehicle.Core.Interfaces;using Vehicle.IO.Interfaces;using Vehicle.Models;using Vehicle.Models.Interfaces;namespace Vehicle.Core{    public class Engine : IEngine    {        private readonly IReader reader;        private readonly IWriter writer;        private readonly ICollection<IVehicle> vehicles;        public Engine(IReader reader, IWriter writer)        {            this.reader = reader;            this.writer = writer;            vehicles = new List<IVehicle>();        }        public void Run()        {            string[] tokens = reader.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);            vehicles.Add(new Car(double.Parse(tokens[1]), double.Parse(tokens[2])));            tokens = reader.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);            vehicles.Add(new Truck(double.Parse(tokens[1]), double.Parse(tokens[2])));            int commandCount = int.Parse(Console.ReadLine());            for (int i = 0; i < commandCount; i++)            {                try                {                    ProcessCommand();                }                catch (Exception ex)                {                    writer.WriteLine(ex.Message);                }            }            foreach (var vehicle in vehicles)            {                writer.WriteLine(vehicle.ToString());            }        }        private void ProcessCommand()        {            string[] commandTokens = reader.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);            string command = commandTokens[0];            string vehicleType = commandTokens[1];            IVehicle vehicle = vehicles.FirstOrDefault(v => v.GetType().Name ==  vehicleType);            if (command == "Drive")            {                double distance = double.Parse(commandTokens[2]);                writer.WriteLine(vehicle.Drive(distance));            }            else if (command == "Refuel")            {                double amount = double.Parse(commandTokens[2]);                vehicle.Refuel(amount);            }        }    }}