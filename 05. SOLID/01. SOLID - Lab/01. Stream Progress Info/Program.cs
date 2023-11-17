namespace P01.Stream_Progress
{
    public class Program
    {
        static void Main()
        {
            //Music music = new("john", "this", 23, 3);

            Music song = new Music("john", "this", 23, 3);

            StreamProgressInfo stream = new StreamProgressInfo(song);
            Console.WriteLine(stream.CalculateCurrentPercent());
        }
    }
}
