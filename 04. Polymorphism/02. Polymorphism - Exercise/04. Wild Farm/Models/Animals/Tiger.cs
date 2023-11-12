using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WildFarm.Models.Foods;

namespace WildFarm.Models.Animals
{
    public class Tiger : Feline
    {
        public const double MouseWeightMultiplyer = 1;

        public Tiger(string name, double weight,string livingRegion, string breed) 
            : base(name, weight,livingRegion, breed)
        {
            LivingRegion = livingRegion;
        }
        public string LivingRegion { get; set; }

        protected override IReadOnlyCollection<Type> PreferredFoodTypes
            => new HashSet<Type>() { typeof(Meat) };

        protected override double WeightMultiplier => MouseWeightMultiplyer;

        public override string ProduceSound()
        {
            return "ROAR!!!";
        }
    }
}
