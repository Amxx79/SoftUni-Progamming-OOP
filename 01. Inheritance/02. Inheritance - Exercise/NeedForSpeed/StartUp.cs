namespace NeedForSpeed
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            RaceMotorcycle raceMotorcycle = new(120, 100);
            raceMotorcycle.Drive(10);
        }
    }
}
