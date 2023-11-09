using BirthdayCelebrations.Models.Interfaces;

namespace BorderControl.Models.Interfaces
{
    public class Human : IHumanable
    {
        public Human(string name, int age, string id, string[] birthday)
        {
            Name = name;
            Id = id;
            Age = age;
            BirthDay = birthday;
        }

        public string Name { get; set; }
        public int Age { get; set; }
        public string Id { get; set; }
        public string[] BirthDay { get; set; }

    }
}
