using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WildFarm.Models.Interfaces;

namespace WildFarm.Models.Animals
{
    public abstract class Animal : IAnimal
    {
        public Animal(string name, double weight)
        {
            Name = name;
            Weight = weight;
        }

        public string Name { get; set; }
        public int FoodEaten { get; set; }
        public double Weight { get; set; }

        protected abstract IReadOnlyCollection<Type> PreferredFoodTypes { get; }
        protected abstract double WeightMultiplier { get; }

        public void Eat(IFood food)
        {
            if (!PreferredFoodTypes.Any(pf => food.GetType().Name == pf.Name))
            {
                throw new ArgumentException($"{this.GetType().Name} does not eat {food.GetType().Name}!");
            }

            Weight += food.Quantity * WeightMultiplier;

            FoodEaten += food.Quantity;
        }

        public abstract string ProduceSound();
    }
}
