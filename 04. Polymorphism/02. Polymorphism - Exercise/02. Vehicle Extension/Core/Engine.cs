﻿using Vehicle.Core.Interfaces;

            if (vehicle == null)
            {
                throw new ArgumentException("Invalid vehicle type");
            }
            {
                double distance = double.Parse(commandTokens[2]);