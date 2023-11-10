using System.Security.Cryptography.X509Certificates;

namespace Shapes
{
    public class StartUp
    {
        static void Main()
        {
            Shape shape = new Circle(3);
            Shape rect = new Rectangle(3, 3);

            Console.WriteLine(shape.Draw());
            Console.WriteLine(rect.Draw());
        }
    }
}