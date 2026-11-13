using System;

namespace LaboratoryWork3_4
{
    public class BankAccount
    {
        public string AccountNumber { get; private set; }
        public decimal Balance { get; private set; }
        public decimal CreditLimit { get; private set; }

        public event EventHandler<OverdraftEventArgs> OverdraftOccurred;

        public BankAccount(string accountNumber, decimal initialBalance, decimal creditLimit)
        {
            AccountNumber = accountNumber;
            Balance = initialBalance;
            CreditLimit = creditLimit;
        }

        public void Deposit(decimal amount)
        {
            if (amount <= 0) throw new ArgumentException("Сума поповнення має бути більшою за нуль.");
            Balance += amount;
        }

        public void Withdraw(decimal amount)
        {
            if (amount <= 0) throw new ArgumentException("Сума зняття має бути більшою за нуль.");

            if (Balance + CreditLimit < amount)
            {
                OnOverdraftOccurred(new OverdraftEventArgs(AccountNumber, Balance, CreditLimit, amount));
                throw new InvalidOperationException("Операцію зняття відхилено через перевищення ліміту овердрафту.");
            }

            Balance -= amount;
        }

        protected virtual void OnOverdraftOccurred(OverdraftEventArgs e)
        {
            OverdraftOccurred?.Invoke(this, e);
        }
    }
}