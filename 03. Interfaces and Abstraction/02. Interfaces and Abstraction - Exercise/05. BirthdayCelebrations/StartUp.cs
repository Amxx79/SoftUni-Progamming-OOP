using BirthdayCelebrations.Models;
using BorderControl.Models.Interfaces;
using System.Text;

string command;
List<string[]> years = new();

while ((command = Console.ReadLine()) != "End")
{
    string[] infoData = command.Split(" ", StringSplitOptions.RemoveEmptyEntries);
    string type = infoData[0];
    string name = infoData[1];

    if (type == "Citizen")
    {
        int age = int.Parse(infoData[2]);
        string id = infoData[3];
        string[] birthday = infoData[4].Split("/", StringSplitOptions.RemoveEmptyEntries);
        Human human = new(name, age, id, birthday);
        years.Add(birthday);
    }
    else if (type == "Pet")
    {
        string[] birthday = infoData[2].Split("/", StringSplitOptions.RemoveEmptyEntries);
        Pet pet = new(name, birthday);
        years.Add(birthday);
    }
    else if (type == "Robot")
    {
        string id = infoData[2];
        Robot robot = new(name, id);
    }
}
string yearToCompare = Console.ReadLine();

foreach (var year in years)
{
    string currentYear = year[2];
    if (currentYear == yearToCompare)
    {
        Console.Write(year[0] + "/");
        Console.Write(year[1] + "/");
        Console.Write(year[2]);
        Console.WriteLine();
    }
}