using System;
namespace BANK.Model
{
    public class BankAccount
    {


        public string AccountType { get; set; }
        public decimal Balance { get; set; }
        public string Id { get; set; }
        public string UserId { get; set; }
        public string CurrencyId { get; set; }

        public BankAccount(string accountType, decimal balance, string id, string userId, string currencyId)
        {
            AccountType = accountType;
            Balance = balance;
            Id = id;
            UserId = userId;
            CurrencyId = currencyId;
        }
    }
}

