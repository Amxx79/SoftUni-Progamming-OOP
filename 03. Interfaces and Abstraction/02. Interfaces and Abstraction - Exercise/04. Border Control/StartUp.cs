using BorderControl.Models.Interfaces;


string command;
List<string> enteringThings = new List<string>();

while ((command = Console.ReadLine()) != "End")
{
    string[] infoData = command.Split(" ", StringSplitOptions.RemoveEmptyEntries);

    string name = infoData[0];

    if (infoData.Length == 3)
    {
        int age = int.Parse(infoData[1]);
        string id = infoData[2];
        Human human = new(name, age, id);
        enteringThings.Add(human.Id);
    }
    else if (infoData.Length == 2)
    {
        string id = infoData[1];
        Robot robot = new(name, id);
        enteringThings.Add(robot.Id);
    }
}

string fakeEndIds = Console.ReadLine();
List<string> allFakeIds = new();
foreach (var id in enteringThings)
{
    if (id.EndsWith(fakeEndIds))
    {
        Console.WriteLine(id);
    }
}
