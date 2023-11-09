using FoodShortage.Models.Interfaces;

namespace FoodShortage.Models
{
    public class Robot : IRobotrable
    {
        public Robot(string name, string id)
        {
            Name = name;
            Id = id;
        }

        public string Name { get; set; }
        public string Id { get; set; }
    }
}
