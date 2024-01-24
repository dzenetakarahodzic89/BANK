using System;
namespace BANK.Model.Enums
{
    [System.Flags]
    public enum TransactionType : byte
    {
        Savings = 1,
        Current,
        FixedDeposit,

    }
}

