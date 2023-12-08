using BankLoan.Core.Contracts;
using BankLoan.Models;
using BankLoan.Models.Contracts;
using BankLoan.Repositories;
using BankLoan.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLoan.Core
{
    public class Controller : IController
    {
        private LoanRepository loansRepository;
        private BankRepository banksRepository;

        public Controller()
        {
            loansRepository = new();
            banksRepository = new();
        }
        public string AddBank(string bankTypeName, string name)
        {
            if (bankTypeName == "CentralBank")
            {
                CentralBank bank = new(name);
                banksRepository.AddModel(bank);
                return $"{bankTypeName} is successfully added.";
            }
            else if (bankTypeName == "BranchBank")
            {
                BranchBank bank = new(name);
                banksRepository.AddModel(bank);
                return $"{bankTypeName} is successfully added.";
            }
            else
            {
                return "Invalid bank type.";
            }
        }

        public string AddClient(string bankName, string clientTypeName, string clientName, string id, double income)
        {
            IClient currentClient;

            //Checking The Client's Type
            if (clientTypeName == nameof(Student))
            {
                currentClient = new Student(clientName, id, income);
            }
            else if (clientTypeName == nameof(Adult))
            {
                currentClient = new Adult(clientName, id, income);
            }
            else
            {
                throw new ArgumentException(ExceptionMessages.ClientTypeInvalid);
            }

            IBank currentBank = banksRepository.FirstModel(bankName);

            //Client Type Name Is Not A Valid Client Type Of The Current Bank
            if (currentBank.GetType().Name == nameof(BranchBank) && currentClient.GetType().Name == nameof(Adult)
                || currentBank.GetType().Name == nameof(CentralBank) && currentClient.GetType().Name == nameof(Student))
            {
                return OutputMessages.UnsuitableBank;
            }

            //Successfully Adding The Client To The Bank
            currentBank.AddClient(currentClient);
            return String.Format(OutputMessages.ClientAddedSuccessfully, clientTypeName, bankName);

        }

        public string AddLoan(string loanTypeName)
        {
            if (loanTypeName == "MortgageLoan")
            {
                MortgageLoan loan = new();
                loansRepository.AddModel(loan);
                return $"{loanTypeName} is successfully added.";
            }
            else if (loanTypeName == "StudentLoan")
            {
                StudentLoan loan = new();
                loansRepository.AddModel(loan);
                return $"{loanTypeName} is successfully added.";
            }
            else
            {
                throw new ArgumentException(ExceptionMessages.LoanTypeInvalid);
            }
        }

        public string FinalCalculation(string bankName)
        {
            IBank bank = banksRepository.FirstModel(bankName);
            double funds = 0;
            foreach (var loan in bank.Loans)
            {
                funds += loan.Amount;
            }
            foreach (var client in bank.Clients)
            {
                funds += client.Income;
            }
            return $"The funds of bank {bank.Name} are {funds:F2}.";
        }

        public string ReturnLoan(string bankName, string loanTypeName)
        {
            ILoan currentLoan = loansRepository.FirstModel(loanTypeName);

            if (currentLoan == null)
            {
                return string.Format(ExceptionMessages.MissingLoanFromType, loanTypeName);
            }

            //Successfully Returning The Loan
            IBank currentBank = banksRepository.FirstModel(bankName);
            currentBank.AddLoan(currentLoan);
            loansRepository.RemoveModel(currentLoan);

            return String.Format(OutputMessages.LoanReturnedSuccessfully, loanTypeName, bankName);
        }

        public string Statistics()
        {
            StringBuilder sb = new();
            foreach(var bank in banksRepository.Models)
            {
                sb.AppendLine(bank.GetStatistics());
            }
            return sb.ToString().Trim();
        }
    }
}
