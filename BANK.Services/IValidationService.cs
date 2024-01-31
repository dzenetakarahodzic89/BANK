using System;
using BANK.Model;
namespace BANK.Services
{
	public interface IValidationService
	{

        public void ValidateName(string name);
        public void ValidateSurName(string surName);
        public void ValidatePassword(string password);
        public void ValidateEmail(string email);
        public void ValidatePassword(byte[] password);
        public void validatePersonIsEmployee(Person? person);
    }
}

