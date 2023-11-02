using System;
using System.Collections.Generic;

namespace PersonsInfo
{
    public class StartUp
    {
        static void Main()
        {
            int n = int.Parse(Console.ReadLine());
            List<Person> list = new List<Person>();
            Team firstTeam = new("SoftUni");

            for (int i = 0; i < n; i++)
            {
                string[] personInfo = Console.ReadLine().Split();
                string fN = personInfo[0];
                string lN = personInfo[1];
                int age = int.Parse(personInfo[2]);
                decimal sl = decimal.Parse(personInfo[3]);

                Person person = new Person(fN, lN, age, sl);
                list.Add(person);
                firstTeam.AddPlayer(person);
            }

            Console.WriteLine($"First team has {firstTeam.FirstTeam.Count} players.");
            Console.WriteLine($"First team has {firstTeam.ReserveTeam.Count} players.");
        }
    }
}