using System;
namespace BANK.Model.Enums
{
    [System.Flags]
    public enum ActionType : byte
    {
        CreateAccount = 1,
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

