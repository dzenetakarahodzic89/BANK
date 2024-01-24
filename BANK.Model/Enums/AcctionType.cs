using System;
namespace BANK.Model.Enums
{
    public enum AcctionType
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

