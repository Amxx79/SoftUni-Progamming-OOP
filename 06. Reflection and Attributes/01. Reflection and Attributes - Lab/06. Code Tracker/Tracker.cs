using System.Reflection;

namespace AuthorProblem
{
    public class Tracker
    {
        public void PrintMethodsByAuthor()
        {
            Type startUp = typeof(StartUp);
            MethodInfo[] methods = startUp.GetMethods();

            foreach (var method in methods)
            {
                AuthorAttribute[] attributes = method.GetCustomAttributes<AuthorAttribute>().ToArray();

                if (attributes.Length > 0)
                {
                    foreach (var attribute in attributes)
                    {
                        Console.WriteLine($"{method.Name} is written by {attribute.Name}");
                    }
                }
            }
        }
    }
}
