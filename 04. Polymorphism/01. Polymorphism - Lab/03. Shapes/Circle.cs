using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shapes
{
    public class Circle : Shape
    {
        private int radius;

        public Circle(int radius)
        {
            this.radius = radius;
        }

        public override double CalculateArea()
        {
            return Math.PI * this.radius * this.radius;
        }

        public override double CalculatePerimeter()
        {
            return 2 * Math.PI * radius;
        }
    }
}
