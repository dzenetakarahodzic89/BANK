using System;
using BANK.DAO;
using BANK.Model;
using BANK.Model.Enums;

namespace BANK.Services
{
	public class HistoryOfActionsService :IHistoryOfActionsService
	{

        private readonly IHistoryOfActionsDAO historyOfActionsDAO;
        private readonly IPersonDAO personDAO;

        public HistoryOfActionsService(IHistoryOfActionsDAO historyOfActionsDAO,IPersonDAO personDAO)
        {
            this.historyOfActionsDAO = historyOfActionsDAO;
            this.personDAO = personDAO;
        }

        public List<HistoryOfActions> getAllHistoryOfAction(string employeeId)
        {

            var employee = personDAO?.getById(employeeId);

            if (employee == null || !(employee is Employee))
            {
                throw new UnauthorizedAccessException("The person is not an employee.");
            }

            var castedEmployee = (Employee)employee;

            if (castedEmployee.EmployeeType == EmployeeType.Administrator ||
                castedEmployee.EmployeeType == EmployeeType.Manager)
            {
                return historyOfActionsDAO.getAllHistoryOfAction();
            }
            else
            {
                throw new UnauthorizedAccessException("The employee does not have permission to access the action history.");
            }

        }


























    }
}

