using FoodShortage.Models;
using FoodShortage.Models.Interfaces;
using System.Runtime.InteropServices;
using System.Text;

int n = int.Parse(Console.ReadLine());
List<IBuyer> list = new();
for (int i = 0; i < n; i++)
{
    string[] infoData = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
    string name = infoData[0];

    if (infoData.Length == 4)
    {
        int age = int.Parse(infoData[1]);
        string id = infoData[2];
        string[] birthday = infoData[3].Split("/", StringSplitOptions.RemoveEmptyEntries);
        Human human = new(name, age, id, birthday);
        list.Add(human);
    }
    else if (infoData.Length == 3)
    {
        int age = int.Parse(infoData[1]);
        string group = infoData[2];
        Rebel rebel = new(name, age, group);
        list.Add(rebel);
    }
}


string command;
int totalPurchased = 0;
while ((command = Console.ReadLine()) != "End")
{
    foreach (var guy in list)
    { 
        if (guy.Name == command)
        {
            totalPurchased += guy.BuyFood();
        }
    }
}
Console.WriteLine(totalPurchased);