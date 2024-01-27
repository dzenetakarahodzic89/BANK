using BANK.Model.Enums;
using System;
namespace BANK.Model
{
    public class Transaction
    {

        public string Id { get; set; }
        public DateTime TransactionDate { get; set; }
        public TransactionType TransactionType { get; set; }
        public string ToAccountId { get; set; }
        public string FromAccountId { get; set; }
        public decimal TransactionAmount { get; set; }
        public TransactionStatus TransactionStatus { get; set; }
        public string EmployeeId { get; set; }

        public Transaction(string id, DateTime transactionDate, TransactionType transactionType, string toAccountId, string fromAccountId, decimal transactionAmount, TransactionStatus transactionStatus, string employeeId)
        {
            Id = id;
            TransactionDate = transactionDate;
            TransactionType = transactionType;
            ToAccountId = toAccountId;
            FromAccountId = fromAccountId;
            TransactionAmount = transactionAmount;
            TransactionStatus = transactionStatus;
            EmployeeId = employeeId;
        }
    }
}

