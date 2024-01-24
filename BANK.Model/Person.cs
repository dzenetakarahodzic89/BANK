using BANK.Model.Enums;
using System;
namespace BANK.Model;

public class Person
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string SurName { get; set; }
    public Gender Gender { get; set; }
    public byte[] Password { get; set; }
    public byte[] Salt { get; set; }
    public bool isActive { get; set; }
    public DateTime CreateOn { get; set; }
    public DateTime? DeleteOn { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }

    public Person(string id, string name, string surName, Gender gender, byte[] password, byte[] salt, bool isActive, DateTime createOn, DateTime? deleteOn, string email, string address, string phone)
    {
        Id = id;
        Name = name;
        SurName = surName;
        Gender = gender;
        Password = password;
        Salt = salt;
        this.isActive = isActive;
        CreateOn = createOn;
        DeleteOn = deleteOn;
        Email = email;
        Address = address;
        Phone = phone;
    }
}

