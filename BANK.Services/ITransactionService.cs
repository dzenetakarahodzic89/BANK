using System;
using BANK.DAO;
using BANK.Model;
using BANK.Model.Enums;

namespace BANK.Services
{
	public interface ITransactionService
	{
        public List<Transaction> getAllTransaction();
        public Transaction? createTransaction(string userId, TransactionType transactionType, string toAccountId, string fromAccountId, decimal transactionAmount);

    }
}

