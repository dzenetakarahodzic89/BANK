using BANK.Model;
using BANK.Model.Enums;
using System;
using System.Globalization;

namespace BANK.DAO
{
	public class BankAccountDAO :IBankAccountDAO
	{

        List<BankAccount> bankAccount{ get; set; } = new();
        private const string BankAccountPath = "bankAccount.csv";


        public void save()
        {
            //var bankAccount = new List<BankAccount> {

            //    new BankAccount(accountType:AccountType.Current,balance:100.00m,id:"MAC",userId:"ID2",currencyId:"BAM"),
            //    new BankAccount(accountType:AccountType.Savings,balance:50.00m,id:"DOM",userId:"ID2",currencyId:"USD")
            //};

            if (bankAccount == null || bankAccount.Count == 0)
            {
                throw new InvalidOperationException("Podaci o bankovnim računima nisu dostupni. Molimo popunite informacije o korisniku.");
            }

            string path = Path.Combine(Environment.CurrentDirectory, BankAccountPath);

            using (var writer = new StreamWriter(path))
            {
                writer.WriteLine("AccountType,Balance,Id,UserId,CurrencyId");

                foreach (var account in bankAccount)
                {
                    writer.WriteLine($"{account.AccountType.ToString()},{account.Balance},\"{account.Id}\",\"{account.UserId}\",\"{account.CurrencyId}\"");
                }
            }
        }


        public List<BankAccount> load()
        {
            var loadedAccounts = new List<BankAccount>();
            string path = Path.Combine(Environment.CurrentDirectory, BankAccountPath);

            if (!File.Exists(path))
            {
                throw new FileNotFoundException($"Datoteka nije pronađena na putanji: {path}");
            }

            using (var reader = new StreamReader(path))
            {
                reader.ReadLine();

                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    var values = line.Split(',');


                    if (values.Length == 5)
                    {
                        var account = new BankAccount(
                            accountType:(AccountType)Enum.Parse(typeof(AccountType), values[0]),
                            balance: decimal.Parse(values[1], CultureInfo.InvariantCulture),
                            id: values[2].Trim('\"'),
                            userId: values[3].Trim('\"'),
                            currencyId: values[4].Trim('\"')
                        );

                        loadedAccounts.Add(account);
                    }
                    else
                    {

                        Console.WriteLine("Red u CSV datoteci nije ispravnog formata.");
                    }
                }
            }

            return loadedAccounts;

        }

        public BankAccount? getBankAccountById(string id)
        {
            return bankAccount.Where(c => c.Id.Equals(id)).FirstOrDefault();
        }

        public List<BankAccount> getAllBankAccount()
        {
            return bankAccount;
        }

        public BankAccount? removeById(string id)
        {
            var bankAccountToRemove = bankAccount.Find(a => a.Id.Equals(id));

            if (bankAccountToRemove != null)
            {
                bankAccount.Remove(bankAccountToRemove);
            }

            return bankAccountToRemove;
        }

        public BankAccount? createBankAccount(AccountType accountType, decimal balance, string id, string userId, string currencyId)
        {

            //TODO: generisati ID da bude string od 16 brojeva
            var bankAccountToEdit = bankAccount.Find(a => a.Id.Equals(id));

            if (bankAccountToEdit != null)
            {

                throw new Exception($"This currency Id : {id} , already exist");
            }


            BankAccount newBankAccount = new BankAccount(accountType, balance, id, userId, currencyId);
            bankAccount.Add(newBankAccount);

            return newBankAccount;
        }

        public bool editBankAccount(string id, AccountType newAccountType, decimal newBalance, string newUserId, string newCurrencyId)
        {
            var bankAccountToEdit = bankAccount.Find(a => a.Id.Equals(id));

            if (bankAccountToEdit != null)
            {
                bankAccountToEdit.AccountType = newAccountType;
                bankAccountToEdit.Balance = newBalance;
                bankAccountToEdit.UserId = newUserId;
                bankAccountToEdit.CurrencyId = newCurrencyId;

                return true; 
            }

            return false; 
        }



    }
}

