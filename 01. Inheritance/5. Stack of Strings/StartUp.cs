namespace CustomStack
{
    public class StartUp
    {
        public static void Main()
        {
            StackOfStrings stackckck = new();
            stackckck.Push("eee");
            stackckck.AddRange(stackckck);
        }
    }
}