using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03._Shopping_Spree
{
    public class Person
    {
        private string name;
        private decimal money;

        public Person(string name, decimal money)
        {
            this.Name = name;
            this.Money = money;
            this.Bag = new List<Product>();
        }

        public string Name
        {
            get => name;

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Name cannot be empty");
                }
                name = value;
            }
        }
        public decimal Money
        {
            get => money;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Money cannot be negative");
                }
                money = value;
            }
        }

        private List<Product> Bag { get; set; }

        public void AddProduct(Product product)
        {
            Bag.Add(product);
        }


        public override string ToString()
        {
            string productsString = Bag.Any()
                 ? string.Join(", ", Bag.Select(p => p.Name))
                 : "Nothing bought";

            return $"{Name} - {productsString}";
        }
    }
}
