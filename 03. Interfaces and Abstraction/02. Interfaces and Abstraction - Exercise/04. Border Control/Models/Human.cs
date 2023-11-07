namespace BorderControl.Models.Interfaces
{
    public class Human : IHumanable
    {
        public Human(string name, int age, string id)
        {
            Name = name;
            Id = id;
            Age = age;
        }

        public string Name { get; set; }
        public int Age { get; set; }
        public string Id { get; set; }
    }
}
