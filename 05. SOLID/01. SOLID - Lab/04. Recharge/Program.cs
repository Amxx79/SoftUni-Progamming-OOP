namespace P04.Recharge
{
    class Program
    {
        static void Main()
        {
            Employee emp = new("22");
            Robot rob = new("23", 100);

            emp.Work(12);
            emp.Sleep();

            rob.Recharge();
            rob.Work(24);

        }
    }
}
