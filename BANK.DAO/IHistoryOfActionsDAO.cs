using System;
using BANK.Model;
using BANK.Model.Enums;

namespace BANK.DAO
{
	public interface IHistoryOfActionsDAO
	{
        public void save();
        public void load();
        public HistoryOfActions? createHistoryOfActions( string accountId, DateTime actionDate, ActionType actionType, string employeeId, string? bankAccountId);

    }
}

