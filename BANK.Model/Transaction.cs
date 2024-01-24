using BANK.Model.Enums;
using System;
namespace BANK.Model
{
    public class Transaction
    {

        public string Id { get; set; }
        public DateTime TransactionDate { get; set; }
        public TransactionType TransactionType { get; set; }
        public string ToAccountType { get; set; }
        public string FromAccountType { get; set; }
        public decimal TransactionAmount { get; set; }
        public TransactionStatus TransactionStatus { get; set; }
        public Employee Employee { get; set; }

        public Transaction(string id, DateTime transactionDate, TransactionType transactionType, string toAccountType, string fromAccountType, decimal transactionAmount, TransactionStatus transactionStatus, Employee employee)
        {
            Id = id;
            TransactionDate = transactionDate;
            TransactionType = transactionType;
            ToAccountType = toAccountType;
            FromAccountType = fromAccountType;
            TransactionAmount = transactionAmount;
            TransactionStatus = transactionStatus;
            Employee = employee;
        }
    }
}

