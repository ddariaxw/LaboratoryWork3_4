using System;

namespace LaboratoryWork3_4
{
    public class OverdraftEventArgs : EventArgs
    {
        public string AccountNumber { get; }
        public decimal CurrentBalance { get; }
        public decimal CreditLimit { get; }
        public decimal RequestedArgumentAmount { get; }
        public decimal OverdraftAmount { get; }

        public OverdraftEventArgs(string accountNumber, decimal currentBalance, decimal creditLimit, decimal requestedAmount)
        {
            AccountNumber = accountNumber;
            CurrentBalance = currentBalance;
            CreditLimit = creditLimit;
            RequestedArgumentAmount = requestedAmount;
            OverdraftAmount = requestedAmount - (currentBalance + creditLimit);
        }
    }
}