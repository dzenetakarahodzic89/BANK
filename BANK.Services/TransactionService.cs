using BANK.DAO;
using BANK.Model;
using BANK.Model.Enums;
namespace BANK.Services
{
    public class TransactionService : ITransactionService
    {

        public readonly ITransactionDAO transactionDAO;
        public readonly IValidationService validationService;
        public readonly IBankAccountDAO bankAccountDAO;
        public readonly IPersonDAO personDAO;
        public readonly IHistoryOfActionsDAO historyOfActionsDAO;

        public TransactionService(ITransactionDAO transactionDAO,
                IValidationService validationService,
                IBankAccountDAO bankAccountDAO,
                IPersonDAO personDAO,
                IHistoryOfActionsDAO historyOfActionsDAO
                )
        {
            this.transactionDAO = transactionDAO;
            this.validationService = validationService;
            this.bankAccountDAO = bankAccountDAO;
            this.personDAO = personDAO;
            this.historyOfActionsDAO = historyOfActionsDAO;
        }


        public List<Transaction> getAllTransaction()
        {
            return transactionDAO.getAllTransaction();
        }

        public List<Transaction> getAllTransactionsByUserId() {
            throw new NotImplementedException();
        }

        public Transaction? createTransaction(string userId, TransactionType transactionType, string toAccountId, string fromAccountId, decimal transactionAmount)
        {
            //Dodati validacije za user id, za racun, za broj racuna, i za stanje na racunu

            var fromBankAccount = bankAccountDAO.getBankAccountById(fromAccountId);
            if(fromBankAccount == null)
            {
                throw new KeyNotFoundException($"bank account with id={fromAccountId} not fount");
            }
            var toBankAccount = bankAccountDAO.getBankAccountById(toAccountId);
            if (toBankAccount == null)
            {
                throw new KeyNotFoundException($"bank account with id={toAccountId} not fount");
            }
            if ( !fromBankAccount.UserId.Equals(userId))
            {
                throw new UnauthorizedAccessException("User is not the owner of the account!");
            }

            if (fromBankAccount.Balance < transactionAmount)
            {
                throw new Exception("insufficient funds");
            }
            if(!fromBankAccount.CurrencyId.Equals(toBankAccount.CurrencyId))
            {
                throw new Exception("Currency of the accounts must be same!");
            }
            if (fromBankAccount.AccountType.Equals(AccountType.Savings))
            {
                throw new Exception("Cannot transfer funds from saving account");
            }
            var newTransaction = transactionDAO.createTransaction(DateTime.Now, transactionType, toAccountId, fromAccountId, transactionAmount, TransactionStatus.Pending,null);
            return newTransaction;
        }


        public Transaction? ApproveTransaction( string employeeId, string transactionId)
        {
            var currentEmployee = personDAO.getById(employeeId);
            validationService.validatePersonIsEmployee(currentEmployee);
            var transactionToApprove = transactionDAO.getTransactionById(transactionId);
            if(transactionToApprove == null)
            {
                throw new KeyNotFoundException($"Transaction with id {transactionId} not found");

            }
            if (!transactionToApprove.TransactionStatus.Equals(TransactionStatus.Approved))
            {
                throw new Exception("Transaction state invalid!");
            }
            var fromBankAccount = bankAccountDAO.getBankAccountById(transactionToApprove.FromAccountId);
            if (fromBankAccount == null)
            {
                throw new KeyNotFoundException($"bank account with id={transactionToApprove.FromAccountId} not fount");
            }
            if (fromBankAccount.Balance < transactionToApprove.TransactionAmount)
            {
                throw new Exception("insufficient funds");
            }
            var toBankAccount = bankAccountDAO.getBankAccountById(transactionToApprove.ToAccountId);
            if (toBankAccount == null)
            {
                throw new KeyNotFoundException($"bank account with id={transactionToApprove.ToAccountId} not fount");
            }
            decimal newBalanceToAccount = toBankAccount.Balance + transactionToApprove.TransactionAmount;
            decimal newBalanceFromAccount = fromBankAccount.Balance - transactionToApprove.TransactionAmount;

            bankAccountDAO.editBankAccount(toBankAccount.Id,toBankAccount.AccountType,newBalanceToAccount,toBankAccount.UserId,toBankAccount.CurrencyId);
            bankAccountDAO.editBankAccount(fromBankAccount.Id, fromBankAccount.AccountType, newBalanceFromAccount, fromBankAccount.UserId, fromBankAccount.CurrencyId);
            var approvedTransaction = transactionDAO.editTransaction(transactionToApprove.Id,
                transactionToApprove.TransactionDate,
                transactionToApprove.TransactionType,
                transactionToApprove.ToAccountId,
                transactionToApprove.FromAccountId,
                transactionToApprove.TransactionAmount,
                TransactionStatus.Approved,
                employeeId
                );
            historyOfActionsDAO.createHistoryOfActions(fromBankAccount.UserId,DateTime.Now,ActionType.ApprovedTransaction,employeeId,fromBankAccount.Id);
            return approvedTransaction;
        }

        public Transaction? denyTransaction(string employeeId, string transactionId)
        {
            var currentEmployee = personDAO.getById(employeeId);
            validationService.validatePersonIsEmployee(currentEmployee);
            var transactionToDeny = transactionDAO.getTransactionById(transactionId);
            if (transactionToDeny == null)
            {
                throw new KeyNotFoundException($"Transaction with id {transactionId} not found");
            }
            var deniedTransaction = transactionDAO.editTransaction(transactionToDeny.Id,
                transactionToDeny.TransactionDate,
                transactionToDeny.TransactionType,
                transactionToDeny.ToAccountId,
                transactionToDeny.FromAccountId,
                transactionToDeny.TransactionAmount,
                TransactionStatus.Approved,
                employeeId
             );
            return deniedTransaction;

        }

        public List<Transaction> getAllTransactionsForBankAccount(string bankAccountId) {
            //TODO: dodati provjeru
            return transactionDAO.getAllTransactionsByBankAccountId(bankAccountId);
        }
        public List<Transaction> getAllApprovedTransactionsForBankAccount(string bankAccountId)
        {
            //TODO: dodati provjeru
            return transactionDAO.getAllTransactionsByBankAccountIdAndStatus(bankAccountId,TransactionStatus.Approved);
        }
    }
}
