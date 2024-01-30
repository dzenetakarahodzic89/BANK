using System;
namespace BANK.Model.Enums
{
    [System.Flags]
    public enum ActionType : byte
    {
        CreateClient = 1,
        CreateBankAccount,
        UpdateAccount,
        DeleteAccount,
        RevertTransaction,
        ApprovedTransaction,
        DeniedTransaction,
        PendingTransaction,
        TransactionHistory,
        ActionHistory,
        Back
    }
}

