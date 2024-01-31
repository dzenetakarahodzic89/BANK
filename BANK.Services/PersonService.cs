using System;
using BANK.DAO;
using BANK.Model;
using BANK.Model.Enums;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
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

        public Person createClient(string currentClientId,string name, string surName, string password, Gender gender, bool isActive, string email, string address, string phone, DateTime dueDate) {

            validationService.ValidateEmail(email);
            validationService.ValidateName(name);
            validationService.ValidatePassword(password);
            validationService.ValidateSurName(surName);

            var person = personDAO.getById(currentClientId);

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
            historyOfActionsDAO.createHistoryOfActions(newClient.Id,DateTime.Now,ActionType.CreateClient,currentClientId,null);

            return newClient;

        }

        public Person createEmployee(string currentEmployeeId, string name, string surName, Gender gender,string password, byte[] salt, bool isActive, DateTime createOn, DateTime? deleteOn, string email, string address, string phone, EmployeeType employeeType) 
        {
                validationService.ValidateEmail(email);
                validationService.ValidateName(name);
                validationService.ValidatePassword(password);
                validationService.ValidateSurName(surName);

                var currentEmployee = personDAO.getById(currentEmployeeId); 
                if (currentEmployee == null || !(currentEmployee is Employee))
                {
                    throw new UnauthorizedAccessException("The current employee does not have permission to create a new employee.");
                 }
                Employee newEmployee = personDAO.createEmployee( name, surName, gender, password,isActive, createOn,email, address, phone, employeeType);
                return newEmployee;
        }

                private bool IsAuthorizedToCreateEmployee(EmployeeType employeeType)
                {
                    return employeeType == EmployeeType.CounterWorker || employeeType == EmployeeType.Manager || employeeType == EmployeeType.Administrator;
                }

        public Person authenticateUser(string email,string password) {

            var person = personDAO.getPersonByEmail(email);
            if(person == null)
            {
                throw new Exception($"Person with email {email} does not exist!");
            }
            byte[] hashedPassword = KeyDerivation.Pbkdf2(
                password: password!,
                salt: person.Salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8);
            if (!hashedPassword.Equals(person.Password))
            {
                throw new Exception("Wrong password!");
            }
            return person;
        }

        

    }   
}

