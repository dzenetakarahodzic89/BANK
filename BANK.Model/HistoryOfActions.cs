using BANK.Model.Enums;
using System;
namespace BANK.Model
{
    public class HistoryOfActions
    {

        public string Id { get; set; }
        public string AccountId { get; set; }
        public DateTime ActionDate { get; set; }
        public ActionType ActionType { get; set; }
        public string EmployeeId { get; set; }
        public string? BankAccountId { get; set; }

        public HistoryOfActions(string id, string accountId, DateTime actionDate, ActionType actionType, string employeeId, string? bankAccountId)
        {
            Id = id;
            AccountId = accountId;
            ActionDate = actionDate;
            ActionType = actionType;
            EmployeeId = employeeId;
            BankAccountId = bankAccountId;
        }
    }
}

