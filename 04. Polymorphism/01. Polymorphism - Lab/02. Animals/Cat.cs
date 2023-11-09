using Animals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animals
{
    public class Cat : Animal
    {
        public Cat(string name, string fF) : base(name, fF)
        {
        }

        public override string ExplainSelf()
        {
            StringBuilder sb = new();
            sb.AppendLine(base.ExplainSelf());
            sb.AppendLine("MEEOW");
            return sb.ToString().TrimEnd();
        }
    }
}
