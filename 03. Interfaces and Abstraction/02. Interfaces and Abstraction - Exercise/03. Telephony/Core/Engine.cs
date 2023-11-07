using Telephony.Models.Interfaces;

public class Engine : IEngine
{
    public void Run()
    {
        string[] numbers = Console.ReadLine().Split();
        string[] urls = Console.ReadLine().Split();

        ICallable phone;
        foreach (var number in numbers)
        {
            if (number.Length == 10)
            {
                phone = new Smartphone();
            }
            else
            {
                phone = new StationaryPhone();
            }
            try
            {
                Console.WriteLine(phone.Call(number));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        Smartphone smartphone = new Smartphone();
        foreach (var url in urls)
        {
            try
            {
                Console.WriteLine(smartphone.Browse(url));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}