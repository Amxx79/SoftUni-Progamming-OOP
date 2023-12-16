using BookingApp.Models.Rooms.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Models.Rooms
{
    public abstract class Room : IRoom
    {
        private double pricePerNeight;
        public Room(int bedCapacity)
        {
            BedCapacity = bedCapacity;
            PricePerNight = 0;
        }
        public int BedCapacity { get; private set; }

        public double PricePerNight
        {
            get => pricePerNeight;
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Price cannot be negative!");
                }
                pricePerNeight = value;
            }
        }

        public void SetPrice(double price)
        {
            PricePerNight = price;
        }
    }
}
