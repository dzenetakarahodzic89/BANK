using System;
namespace BANK.Model.Enums
{
    [System.Flags]
    public enum TransactionStatus : byte
    {
        Pending,
        Complited,
        Approved,
        Denied,
        Revert



    }
}


