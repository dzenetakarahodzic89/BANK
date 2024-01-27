using System;
using BANK.Model.Enums;

namespace BANK.Model
{
    public class Client : Person

    {


        public DateTime DueDate { get; set; }



        public Client(string id, string name, string surName, Gender gender, byte[] password, byte[] salt, bool isActive, DateTime createOn, DateTime? deleteOn, string email, string address, string phone, DateTime dateTime)
            : base(id, name, surName, gender, password, salt, isActive, createOn, deleteOn, email, address, phone)
        {

            this.DueDate = dateTime;

        }



    }
}

