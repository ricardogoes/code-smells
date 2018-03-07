using System;

namespace CodeSmells.Dispensables
{
public class InterestCalculator
{
    private readonly decimal _interestRate;

    public InterestCalculator(decimal interestRate)
    {
        this._interestRate = interestRate;
    }

    public void CalculateInterest(Account account)
    {
        if (account.AccountType == "Checking")
        {
            return;
        }
        if (account.AccountType == "Savings")
        {
            decimal interest = account.Balance * this._interestRate;
            account.Balance += interest;
            return;
        }
        throw new InvalidOperationException(string.Format("Unknown Account Type: {0}", account.AccountType));
    }
}
}