using System;
namespace BANK.Model.Enums
{
    [System.Flags]

    public enum AccountType : byte
	{
        Savings = 1,
        Current,
	}
}

