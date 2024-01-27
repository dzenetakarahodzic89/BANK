using System;
namespace BANK.DAO
{
	public interface IHistoryOfActionsDAO
	{
        public void save();
        public void load();

    }
}

