using System;
using BANK.Model.Enums;

namespace BANK.Model
{
    public class Client : Person

    {



        public List<BankAccount> BankAccounts { get; set; }



        public Client(string id, string name, string surName, Gender gender, byte[] password, byte[] salt, bool isActive, DateTime createOn, DateTime? deleteOn, string email, string address, string phone, List<BankAccount> bankAccounts) : base(id, name, surName, gender, password, salt, isActive, createOn, deleteOn, email, address, phone)
        {
            BankAccounts = bankAccounts ?? new List<BankAccount>(); // Ako bankAccounts nije null, koristi proslijeđenu listu; u suprotnom, napravi novu praznu listu
        }






    }
}

