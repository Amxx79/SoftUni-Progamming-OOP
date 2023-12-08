using BankLoan.Models.Contracts;
using BankLoan.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BankLoan.Models
{
    public abstract class Bank : IBank
    {
        private string name;
        //TO LOOKUP
        private List<ILoan> loans;
        private List<IClient> clients;

        public Bank(string name, int capacity)
        {
            loans = new List<ILoan>();
            clients = new List<IClient>();
            Name = name;
            Capacity = capacity;
        }

        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.BankNameNullOrWhiteSpace);
                }
                name = value;
            }
        }

        public int Capacity { get; private set; }

        public IReadOnlyCollection<ILoan> Loans => loans;

        public IReadOnlyCollection<IClient> Clients => clients;

        public void AddClient(IClient Client)
        {
            if (Clients.Count + 1 > Capacity)
            {
                throw new ArgumentException(ExceptionMessages.NotEnoughCapacity);
            }
            clients.Add(Client);
        }

        public void AddLoan(ILoan loan)
        {
            loans.Add(loan);
        }

        public string GetStatistics()
        {
            StringBuilder sb = new();
            sb.AppendLine($"Name: {Name}, Type: {this.GetType().Name}");
            if (Clients.Count == 0)
            {
                sb.AppendLine("Clients: none");
            }
            else
            {
                List<string> names = new();
                foreach (var client in clients)
                {
                    names.Add(client.Name);
                }

                sb.AppendLine($"Clients: {string.Join(", ", names)}");
            }
            sb.AppendLine($"Loans: {loans.Count}, Sum of Rates: {SumRates()}");
            return sb.ToString().Trim();
        }

        public void RemoveClient(IClient Client)
        {
            clients.Remove(Client);
        }

        public double SumRates()
        {
            double sum = 0;
            foreach (var loan in loans)
            {
                sum += loan.InterestRate;
            }
            return sum;
        }
    }
}
