namespace BorderControl.Models.Interfaces
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
