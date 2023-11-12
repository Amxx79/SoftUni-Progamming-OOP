﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WildFarm.Models.Foods;

namespace WildFarm.Models.Animals
{
    public class Dog : Mammal
    {
        public const double DogWeightMultiplyer = 0.4;

        public Dog(string name, double weight, string livingRegion) 
            : base(name, weight, livingRegion)
        {
            LivingRegion = livingRegion;
        }

        protected override IReadOnlyCollection<Type> PreferredFoodTypes 
            => new HashSet<Type>() { typeof(Meat) };

        protected override double WeightMultiplier => DogWeightMultiplyer;

        public override string ProduceSound()
        {
            return "Woof!";
        }

        public override string ToString()
        {
            return $"{GetType().Name} [{Name}, {Weight}, {LivingRegion}, {FoodEaten}]";
        }
    }
}
