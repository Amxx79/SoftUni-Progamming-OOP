namespace FoodShortage.Models.Interfaces
{
    public class Pet : IPetable
    {
        public Pet(string name, string[] birthday)
        {
            Name = name;
            BirthDay = birthday;
        }
        public string Name { get; set; }

        public string[] BirthDay { get; set; }
    }
}
