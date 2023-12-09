using BankLoan.Models.Contracts;
using BankLoan.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BankLoan.Repositories
{
    public class LoanRepository : IRepository<ILoan>
    {
        private List<ILoan> loans;
        public IReadOnlyCollection<ILoan> Models => loans;
        public LoanRepository()
        {
            loans = new List<ILoan>();
        }

        public void AddModel(ILoan model)
        {
            loans.Add(model);
        }

        public ILoan FirstModel(string name)
        {
            return loans.FirstOrDefault(l => l.GetType().Name == name);
        }

        public bool RemoveModel(ILoan model)
        {
            return loans.Remove(model);
        }
    }
}
