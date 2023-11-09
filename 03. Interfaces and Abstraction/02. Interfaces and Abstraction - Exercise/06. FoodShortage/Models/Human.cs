namespace FoodShortage.Models.Interfaces
{
    public class Human : IHumanable, IBuyer
    {
        public Human(string name, int age, string id, string[] birthday)
        {
            Name = name;
            Id = id;
            Age = age;
            BirthDay = birthday;
            Food = 0;
        }

        public string Name { get; set; }
        public int Age { get; set; }
        public string Id { get; set; }
        public string[] BirthDay { get; set; }
        public int Food { get; set; }

        public int BuyFood()
        {
            Food += 10;
            return 10;
        }
    }
}
