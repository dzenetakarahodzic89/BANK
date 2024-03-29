﻿using System.Globalization;
using BANK.Model;
using BANK.Model.Enums;

namespace BANK.DAO
{
	public class TransactionDAO : ITransactionDAO
	{

        List<Transaction> transactions { get; set; } = new();
        private const string TransactionPath = "transaction.csv";

        
        public void save()
        {
            //var transactions = new List<Transaction>
            //{
            //    new Transaction("TRX1", DateTime.Now, TransactionType.FixedDeposit, "MAC", "DOM", 100.00m, TransactionStatus.Complited, "EMP1"),
            //    new Transaction("MS1",DateTime.Today,TransactionType.Current,"MAC","DOM",200.00m,TransactionStatus.Approved,"ID1"),
            //    new Transaction("MS2",DateTime.Now,TransactionType.Savings,"DOM","MAC",500.00m,TransactionStatus.Complited,"ID1")

            //};

                string path = Path.Combine(Environment.CurrentDirectory, TransactionPath);

                Console.WriteLine($"{path}");

                using (var writer = new StreamWriter(path))
                {
                    writer.WriteLine("Id,TransactionDate,TransactionType,ToAccountId,FromAccountId,TransactionAmount,TransactionStatus,EmployeeId");

                    foreach (var transaction in transactions)
                    {
                    writer.WriteLine($"\"{transaction.Id}\",\"{transaction.TransactionDate.ToString("o")}\",{transaction.TransactionType},{transaction.ToAccountId},{transaction.FromAccountId},{transaction.TransactionAmount},{transaction.TransactionStatus},{(transaction.EmployeeId!=null ? transaction.EmployeeId : "") }");
                    }
                }
        }


        public List<Transaction> load()
        {
            List<Transaction> transactions = new List<Transaction>();

            if (!File.Exists(TransactionPath))
            {
                Console.WriteLine("The transactions file does not exist.");
                return transactions;
            }

            using (var reader = new StreamReader(TransactionPath))
            {

                reader.ReadLine();

                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    var values = line.Split(',');
                    var transaction = new Transaction(
                        values[0].Trim('\"'),
                        DateTime.ParseExact(values[1].Trim('\"'), "o", CultureInfo.InvariantCulture),
                        (TransactionType)Enum.Parse(typeof(TransactionType), values[2]),
                        values[3],
                        values[4],
                        decimal.Parse(values[5], CultureInfo.InvariantCulture),
                        (TransactionStatus)Enum.Parse(typeof(TransactionStatus), values[6]),
                        string.IsNullOrEmpty(values[7]) ? null : values[7] 
                    );

                    transactions.Add(transaction);
                }
            }

            return transactions;
        }

        public Transaction? getTransactionById(string id)
        {   
            return transactions.Where(transaction=> transaction.Id.Equals(id)).FirstOrDefault();
        }

        public List<Transaction> getAllTransaction()
        {
            return transactions;
        }

        public Transaction? removedById(string id) {

            var transactionToRemove = transactions.FirstOrDefault(t => t.Id.Equals(id));

            if (transactionToRemove != null) {
                transactions.Remove(transactionToRemove);

            }

            return transactionToRemove;
        }

        public Transaction? createTransaction( DateTime transactionDate, TransactionType transactionType, string toAccountId, string fromAccountId, decimal transactionAmount, TransactionStatus transactionStatus, string? employeeId)
        {
            string id = Guid.NewGuid().ToString();
            var transactionToEdit = transactions.Find(c => c.Id.Equals(id));

            if (transactionToEdit != null)
            {

                throw new Exception($"This transaction Id : {id} , already exist");


            }
            Transaction newTransaction = new Transaction(id, transactionDate, transactionType, toAccountId, fromAccountId, transactionAmount, transactionStatus, employeeId);
            transactions.Add(newTransaction);

            return newTransaction;
        }

        public Transaction? editTransaction(string id, DateTime transactionDate, TransactionType transactionType, string toAccountId, string fromAccountId, decimal transactionAmount, TransactionStatus transactionStatus, string employeeId)
        {

            var transactionToEdit = transactions.Find(a => a.Id.Equals(id));

            if (transactionToEdit != null)
            {
                transactionToEdit.TransactionType = transactionType;
                transactionToEdit.TransactionStatus = transactionStatus;
                transactionToEdit.FromAccountId= fromAccountId;
                transactionToEdit.ToAccountId = toAccountId;

                return transactionToEdit;
            }
            return null;

        }


        public Transaction? ApproveTransaction(string id, string employeeId)
        {
            var transactionToApprove = transactions.FirstOrDefault(t => t.Id.Equals(id));

            if (transactionToApprove != null)
            {
                if (transactionToApprove.TransactionStatus == TransactionStatus.Approved)
                {
                    throw new Exception("Transaction is already approved!");
                }
                transactionToApprove.TransactionStatus = TransactionStatus.Approved;
                transactionToApprove.EmployeeId = employeeId;

                return transactionToApprove;
            }

            throw new KeyNotFoundException($"No transaction with id {id}");
        }

        public List<Transaction> getAllTransactionsByBankAccountId(string bankAccountId)
        {
            return transactions.Where(t => t.FromAccountId.Equals(bankAccountId) || t.ToAccountId.Equals(bankAccountId)).ToList();
        }
        public List<Transaction> getAllTransactionsByBankAccountIdAndStatus(string bankAccountId, TransactionStatus status)
        {
            return transactions.Where(t => (t.FromAccountId.Equals(bankAccountId) && t.ToAccountId.Equals(bankAccountId)) && t.TransactionStatus.Equals(status)).ToList();


        }


    }
}

