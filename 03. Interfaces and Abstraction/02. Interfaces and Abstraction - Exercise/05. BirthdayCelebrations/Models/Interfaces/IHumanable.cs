namespace BirthdayCelebrations.Models.Interfaces
{
    public interface IHumanable
    {
        public string Name { get; }
        public int Age { get; }
        public string Id { get; }
        string[] BirthDay { get; }
    }
}
