using BankLoan.Models;
using BankLoan.Models.Contracts;
using BankLoan.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLoan.Repositories
{
    public class BankRepository : IRepository<IBank>
    {
        private List<IBank> banks = new List<IBank>();
        public IReadOnlyCollection<IBank> Models => banks;

        public BankRepository()
        {
            banks = new List<IBank>();
        }

        public void AddModel(IBank model)
        {
            banks.Add(model);
        }

        public IBank FirstModel(string name)
        {
            return banks.FirstOrDefault(l => l.Name == name);
        }

        public bool RemoveModel(IBank model)
        {
            return banks.Remove(model);
        }
    }
}
