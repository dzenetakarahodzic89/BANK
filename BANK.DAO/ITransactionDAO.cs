using BANK.Model;
using BANK.Model.Enums;
using System;
namespace BANK.DAO

{
	public interface ITransactionDAO
	{
        public void save();
        public List<Transaction> load();
        public Transaction? getTransactionById(string id);
        public Transaction? editTransaction(string id, DateTime transactionDate, TransactionType transactionType, string toAccountId, string fromAccountId, decimal transactionAmount, TransactionStatus transactionStatus, string employeeId);
        public Transaction? createTransaction( DateTime transactionDate, TransactionType transactionType, string toAccountId, string fromAccountId, decimal transactionAmount, TransactionStatus transactionStatus, string? employeeId);
        public Transaction? removedById(string id);
        public List<Transaction> getAllTransaction();
        public List<Transaction> getAllTransactionsByBankAccountId(string bankAccountId);
        public List<Transaction> getAllTransactionsByBankAccountIdAndStatus(string bankAccountId, TransactionStatus status);





    }

}

