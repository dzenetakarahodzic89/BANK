using System;
using BANK.Model;
using BANK.Model.Enums;

namespace BANK.Services
{
	public interface IPersonService
	{

        public Person createEmployee(string currentEmployeeId, string name, string surName, string password, Gender gender, bool isActive, string email, string address, string phone, DateTime dueDate);


    }
}

