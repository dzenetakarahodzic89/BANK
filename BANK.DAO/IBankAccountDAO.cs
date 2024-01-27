using System;
using BANK.Model.Enums;
using BANK.Model;

namespace BANK.DAO
{
	public interface IBankAccountDAO
	{
        public List<BankAccount> load();
        public void save();
        public bool editBankAccount(string id, AccountType newAccountType, decimal newBalance, string newUserId, string newCurrencyId);
        public BankAccount? createBankAccount(AccountType accountType, decimal balance, string id, string userId, string currencyId);
        public BankAccount? removeById(string id);
        public List<BankAccount> getAllBankAccount();
        public BankAccount? getBankAccountById(string id);

    }
}

