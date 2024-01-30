using System;
using BANK.DAO;
using BANK.Model;
using BANK.Model.Enums;
using Microsoft.Extensions.DependencyInjection;

namespace BANK.Services
{
	public class PersonService :IPersonService
	{

		private readonly IPersonDAO personDAO;
        private readonly IValidationService validationService;
        private readonly IHistoryOfActionsDAO historyOfActionsDAO;

        public PersonService(IPersonDAO personDAO,IValidationService validationService,IHistoryOfActionsDAO historyOfActionsDAO)
        {
            this.personDAO = personDAO;
            this.validationService = validationService;
            this.historyOfActionsDAO = historyOfActionsDAO;

        }

        public Person createEmployee(string currentEmployeeId,string name, string surName, string password, Gender gender, bool isActive, string email, string address, string phone, DateTime dueDate) {


            validationService.ValidateEmail(email);
            validationService.validateName(name);
            validationService.ValidatePassword(password);
            validationService.ValidateSurName(surName);



            var person = personDAO.getById(currentEmployeeId);

            if (person== null || !(person is Employee)) {
                throw new Exception("Unauthorized!");
            }

            var employee = person as Employee;
            if (employee!.EmployeeType.Equals(EmployeeType.Director) ||
                employee.EmployeeType.Equals(EmployeeType.Manager) ||
                employee.EmployeeType.Equals(EmployeeType.ITSpecialists)) {

                throw new Exception("Employee with current role can't create new client");

            }


            Client newClient =  personDAO.createClient(name,surName,password,gender,isActive,DateTime.Now,email,address,phone,dueDate);
            historyOfActionsDAO.createHistoryOfActions(newClient.Id,DateTime.Now,ActionType.CreateClient,currentEmployeeId,null);

            return newClient;







        }


        

        
    }
}

