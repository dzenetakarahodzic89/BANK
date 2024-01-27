using System;
using BANK.Model.Enums;
namespace BANK.Model
{
    public class EmployeeCSVRow
    {


        public string userId { get; set; }
        public EmployeeType employeeType { get; set; }

        public EmployeeCSVRow(string userId, EmployeeType employeeType)
        {
            this.userId = userId;
            this.employeeType = employeeType;
        }
    }
}

