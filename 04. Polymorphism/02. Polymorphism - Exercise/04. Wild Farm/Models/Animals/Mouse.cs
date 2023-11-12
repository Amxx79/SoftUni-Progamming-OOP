using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WildFarm.Models.Foods;

namespace WildFarm.Models.Animals
{
    public class Mouse : Mammal
    {
        public const double MouseWeightMultiplyer = 0.10;

        public Mouse(string name, double weight, string livingRegion) 
            : base(name, weight, livingRegion)
        {
            LivingRegion = livingRegion;
        }

        protected override IReadOnlyCollection<Type> PreferredFoodTypes 
            => new HashSet<Type>() { typeof(Vegetable), typeof(Fruit) };

        protected override double WeightMultiplier => MouseWeightMultiplyer;

        public override string ProduceSound()
        {
            return "Squeak";
        }

        public override string ToString()
        {
            return $"{GetType().Name} [{Name}, {Weight}, {LivingRegion}, {FoodEaten}]";
        }
    }
}
