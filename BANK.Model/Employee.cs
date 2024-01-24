using System;
using BANK.Model.Enums;

namespace BANK.Model
{
    public class Employee : Person
    {

        public EmployeeType EmployeeType { get; set; }

        public Employee(string id, string name, string surName, Gender gender, byte[] password, byte[] salt, bool isActive, DateTime createOn, DateTime? deleteOn, string email, string address, string phone, EmployeeType employeeType) : base(id, name, surName, gender, password, salt, isActive, createOn, deleteOn, email, address, phone)
        {
            this.EmployeeType = employeeType;
        }






    }
}

