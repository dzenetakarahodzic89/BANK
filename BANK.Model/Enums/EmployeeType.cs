using System;
namespace BANK.Model.Enums
{
    [System.Flags]
    public enum EmployeeType : byte
    {

        CounterWorker,
        Director,
        Administrator,
        Manager,
        ITSpecialists
    }
}

