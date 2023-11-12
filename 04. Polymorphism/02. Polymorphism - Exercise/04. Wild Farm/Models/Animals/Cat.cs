using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WildFarm.Models.Foods;

namespace WildFarm.Models.Animals
{
    public class Cat : Feline
    {
        public const double CarWeightMultiplyer = 0.3;
        public Cat(string name, double weight,string livingRegion, string breed) 
            : base(name, weight,livingRegion, breed)
        {
        }

        protected override IReadOnlyCollection<Type> PreferredFoodTypes 
            => new HashSet<Type>() { typeof(Vegetable), typeof(Meat) };

        protected override double WeightMultiplier => CarWeightMultiplyer;


        public override string ProduceSound()
        {
            return "Meow";
        }
    }
}
