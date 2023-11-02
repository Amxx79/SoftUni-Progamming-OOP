namespace _03._Shopping_Spree
{
    public class StartUp
    {
        static void Main()
        {
            string[] people = Console.ReadLine().Split(";", StringSplitOptions.RemoveEmptyEntries);
            string[] products = Console.ReadLine().Split(";", StringSplitOptions.RemoveEmptyEntries);

            List<Person> personsList = new List<Person>();
            List<Product> productsList = new List<Product>();
            try
            {
                foreach (var p in people)
                {
                    string[] personData = p.Split("=", StringSplitOptions.RemoveEmptyEntries);
                    Person person = new(personData[0], decimal.Parse(personData[1]));
                    personsList.Add(person);
                }

                foreach (var food in products)
                {
                    string[] foodData = food.Split("=", StringSplitOptions.RemoveEmptyEntries);
                    Product product = new(foodData[0], decimal.Parse(foodData[1]));
                    productsList.Add(product);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Environment.Exit(0);
            }

            string command;
            while ((command = Console.ReadLine()) != "END")
            {

                string[] person = command.Split();
                string name = person[0];
                string product = person[1];

                Person currentPerson = personsList.FirstOrDefault(p => p.Name == name);
                Product currentProduct = productsList.FirstOrDefault(p => p.Name == product);

                if (currentPerson.Money >= currentProduct.Cost)
                {
                    Console.WriteLine($"{currentPerson.Name} bought {currentProduct.Name}");
                    currentPerson.Money -= currentProduct.Cost;
                    currentPerson.AddProduct(currentProduct);
                }
                else if (currentPerson.Money < currentProduct.Cost)
                {
                    Console.WriteLine($"{currentPerson.Name} can't afford {currentProduct.Name}");
                }
            }

            

            Console.WriteLine(string.Join(Environment.NewLine, personsList));
        }
    }
}