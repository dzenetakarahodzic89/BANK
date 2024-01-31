using System;
using BANK.Model;
using BANK.Model.Enums;

namespace BANK.Services
{
	public interface IPersonService
	{

        public Person createClient(string currentEmployeeId, string name, string surName, string password, Gender gender, bool isActive, string email, string address, string phone, DateTime dueDate);
        public Person authenticateUser(string email, string password);
        //TODO; popraviti interface
        public Person createEmployee(string id, string name, string surName, Gender gender, string password, byte[] salt, bool isActive, DateTime createOn, DateTime? deleteOn, string email, string address, string phone, EmployeeType employeeType);


    }
}

