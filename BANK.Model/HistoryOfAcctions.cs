using BANK.Model.Enums;
using System;
namespace BANK.Model
{
    public class HistoryOfAcctions
    {

        public string Id { get; set; }
        public string AccountId { get; set; }
        public DateTime AcctionDate { get; set; }
        public AcctionType AcctionType { get; set; }
        public string EmployeeId { get; set; }
        public string BankAccountId { get; set; }

        public HistoryOfAcctions(string id, string accountId, DateTime acctionDate, AcctionType acctionType, string employeeId, string bankAccountId)
        {
            Id = id;
            AccountId = accountId;
            AcctionDate = acctionDate;
            AcctionType = acctionType;
            EmployeeId = employeeId;
            BankAccountId = bankAccountId;
        }
    }
}

