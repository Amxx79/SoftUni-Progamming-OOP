using Stealer;
public class StartUp
{
    static void Main()
    {
        Spy spy = new();

        string result = spy.RevealPrivateMethods("Stealer.Hacker");

        Console.WriteLine(result);
    }
}