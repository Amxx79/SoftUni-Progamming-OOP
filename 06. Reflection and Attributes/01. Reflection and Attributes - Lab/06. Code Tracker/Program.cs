namespace AuthorProblem
{
    public class StartUp
    {
        [Author("Viktor")]
        [Author("Students")]
        public static void Main()
        {
            Tracker tracker = new Tracker();
            tracker.PrintMethodsByAuthor();
        }
        [Author("Ivan")]
        public void DoNothing()
        {

        }
    }
}