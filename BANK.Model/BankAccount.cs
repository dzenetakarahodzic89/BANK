using System;
namespace BANK.Model
{
    public class BankAccount
    {


        public string AccountType { get; set; }
        public decimal Balance { get; set; }
        public string Id { get; set; }
        public Person Person { get; set; }
        public string Currency { get; set; }

        public BankAccount(string accountType, decimal balance, string id, Person person, string currency)
        {
            AccountType = accountType;
            Balance = balance;
            Id = id;
            Person = person;
            Currency = currency;
        }
    }
}

