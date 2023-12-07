using ChristmasPastryShop.Models.Delicacies.Contracts;
using System;
using System.Net.NetworkInformation;

namespace ChristmasPastryShop.Models.Delicacies
{
    public abstract class Delicacy : IDelicacy
    {
        private string name;

        //To Do... public
        protected Delicacy(string name, double price)
        {
            Name = name;
            Price = price;
        }

        public string Name 
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Name cannot be null or whitespace!");
                }

                name = value;
            } 
        }

        public double Price { get; private set; }

        public override string ToString()
        {
            return $"{Name} - {Price:F2} lv";
        }
    }
}
