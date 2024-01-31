using System;
using BANK.Model;

namespace BANK.Services
{
	public interface IHistoryOfActionsService
	{

        public List<HistoryOfActions> getAllHistoryOfAction(string employeeId);

    }
}

