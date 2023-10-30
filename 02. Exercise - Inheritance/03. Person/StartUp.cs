using System;
using System.Xml;

namespace Person
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            string name = Console.ReadLine();
            int age = int.Parse(Console.ReadLine());

            Person person = new(name, age);
            //Child child = new(name, age);
            Console.WriteLine(person.ToString());
        }
    }
}