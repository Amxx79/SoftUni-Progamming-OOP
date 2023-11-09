using Animals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animals
{
    public class Dog : Animal
    {
        public Dog(string name, string fF) : base(name, fF) 
        {
        }

        public override string ExplainSelf()
        {
            StringBuilder ab = new();
            ab.AppendLine(base.ExplainSelf());
            ab.AppendLine("DJAAF");
            return ab.ToString().TrimEnd();
        }
    }
}
