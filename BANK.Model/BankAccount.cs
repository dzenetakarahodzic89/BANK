using System;
using BANK.Model.Enums;
namespace BANK.Model
{
    public class BankAccount
    {


        public AccountType AccountType { get; set; }
        public decimal Balance { get; set; }
        public string Id { get; set; }
        public string UserId { get; set; }
        public string CurrencyId { get; set; }

        public BankAccount(AccountType accountType, decimal balance, string id, string userId, string currencyId)
        {
            AccountType = accountType;
            Balance = balance;
            Id = id;
            UserId = userId;
            CurrencyId = currencyId;
        }
    }
}

