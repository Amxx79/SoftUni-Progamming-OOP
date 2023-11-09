using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animals
{
    public class Animal
    {
        private string name;
        private string favouriteFood;

        public Animal(string name, string fF)
        {
            this.name = name;
            this.favouriteFood = fF;
        }

        public virtual string ExplainSelf()
        {
            return $"I am {name} and my fovourite food is {favouriteFood}";
        }
    }
}
