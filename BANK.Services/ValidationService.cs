using System;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using BANK.Model;

namespace BANK.Services
{
	public class ValidationService : IValidationService
	{


        public void ValidateName(string name)
        {

            if (name == null || name.Equals("") || name.Length < 3)

                throw new Exception("Name is not valid!");


        }

        public void ValidateSurName(string surName) {

            if (surName == null || surName.Equals("") || surName.Length < 3)

                throw new Exception("Surmane is not valid!");
        }

        public void ValidatePassword(string password) {

            if (password == null || !Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d).{8,15}$")) {

               // throw new Exception("Password is not valid!");

            }

        }


        public void ValidateEmail(string email) {

            if (email == null || !Regex.IsMatch(email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")) {

                throw new Exception("Email is not valid!");
            }




        }

        public void ValidatePassword(byte[] password)
        {
            throw new NotImplementedException();
        }

        public void validatePersonIsEmployee(Person? person)
        {
            if (person == null || !(person is Employee))
            {
                throw new UnauthorizedAccessException("User is not employee");
            }
        }
    }
}

