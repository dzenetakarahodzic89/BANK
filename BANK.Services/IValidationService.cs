using System;
namespace BANK.Services
{
	public interface IValidationService
	{

        public void validateName(string name);
        public void ValidateSurName(string surName);
        public void ValidatePassword(string password);
        public void ValidateEmail(string email);



    }
}

