using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WildFarm.Models.Animals
{
    public abstract class Feline : Animal
    {
        public Feline(string name, double weight,string livingRegion, string breed) 
            : base(name, weight)
        {
            LivingRegion = livingRegion;
            Breed = breed;
        }
        public string Breed { get; set; }
        public string LivingRegion { get; set; }

        public override string ToString()
        {
            return $"{GetType().Name} [{Name}, {Breed}, {Weight}, {LivingRegion}, {FoodEaten}]";
        }
    }
}
