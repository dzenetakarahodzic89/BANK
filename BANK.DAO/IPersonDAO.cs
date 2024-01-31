using System;
using BANK.Model;
using BANK.Model.Enums;

namespace BANK.DAO
{
    public interface IPersonDAO
    {
        public void load();
        public Person? getById(string id);
        public void save();
        public List<Person> getAll();
        public void SaveEmployeeCSV();
        public void SaveClientCSV();
        public void loadEmployee();
        public void loadClient();
        public Employee createEmployee(string name, string surName, Gender gender, string password, bool isActive, DateTime createOn,  string email, string address, string phone, EmployeeType employeeType);
        public Client createClient(string name, string surName, string password, Gender gender, bool isActive, DateTime createOn, string email, string address, string phone, DateTime dueDate);
        public Person? getPersonByEmail(string email);

    }
}

