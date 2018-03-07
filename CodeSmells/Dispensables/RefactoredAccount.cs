using System;
using System.Linq;

namespace CodeSmells.Dispensables
{
    public class RefactoredAccount
    {
        public RefactoredAccount (int id, string accountType, decimal balance)
        {
            this.Balance = balance;
            this.AccountType = accountType;
            this.Id = id;
        }

        public int Id { get; private set; }
        public string AccountType { get; private set; }
        public decimal Balance { get; private set; }

        public void CalculateAndApplyInterest()
        {
            var calculator = CalculatorFactory.GetFactory(AccountType);

            var interest = calculator.CalculateInterestForBalance(Balance);

            AdjustBalance(interest);
        }

        private void AdjustBalance(decimal amount)
        {
            // log access to balance
            Balance += amount;
        }
    }
  
    public class CalculatorFactory
    {
        public static ICalculateInterest GetFactory(string accountType)
        {
            // TODO: Implement this method
            throw new NotImplementedException();
        }
    }

    public interface ICalculateInterest
    {
        decimal CalculateInterestForBalance(decimal balance);
    }
}