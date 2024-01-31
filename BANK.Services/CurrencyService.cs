
using BANK.DAO;
using BANK.Model;
using BANK.Model.Enums;
using Microsoft.Extensions.DependencyInjection;

namespace BANK.Services
{

    public class CurrencyService : ICurrencyService

    {


        private readonly ICurrencyDAO currencyDAO;
        private readonly IPersonDAO personDAO;
        public CurrencyService(ICurrencyDAO currencyDAO,IPersonDAO personDAO)

        {
            this.currencyDAO = currencyDAO;
            this.personDAO = personDAO;
        }

        public List<Currency> getAllCurrencies()
        {
            return currencyDAO.getAllCurrency();
        }

        public Currency? createCurrency(string EmployeeId, string id, string name, double exchangeRate, double exchangeRateB, double exchangeRateS)
        {

            if (id == null || EmployeeId.Equals("") || name == null || name.Equals("") || name.Length < 5
                || exchangeRate <= 0 || exchangeRateB <= 0 || exchangeRateS <= 0)

            {
                throw new Exception("Currency data is not valid! Please try again.");
            }

            var employee = personDAO?.getById(EmployeeId);

            if (employee == null || !(employee is Employee))
            {
                throw new UnauthorizedAccessException("The person is not an employee.");
            }

            var castedEmployee = (Employee)employee;

            if(!(castedEmployee.EmployeeType == EmployeeType.Administrator ||
                castedEmployee.EmployeeType == EmployeeType.Manager))
            {
                throw new UnauthorizedAccessException();
            }

            var currency = currencyDAO.createCurrency(EmployeeId, name, exchangeRate, exchangeRateB, exchangeRateS);
            return currency;
           

        }

            public Currency? editCurrency(string userId, string id, string name, double exchangeRate, double exchangeRateB, double exchangeRateS)
            {
                if (id == null || id.Equals("") || name == null || name.Equals("") || name.Length < 5
               || exchangeRate <= 0 || exchangeRateB <= 0 || exchangeRateS <= 0)
                {
                    throw new Exception("Currency data is not valid! Please try again.");
                }

                var currency = currencyDAO.editCurrency(id, name, exchangeRate, exchangeRateB, exchangeRateS);
                return currency;
            }


            public Currency? removeById( string EmployeeId, string id)
            {
                //TODO: dodati uslove za provjeru

                var currency = currencyDAO.removeById(id);
                return currency;
            }
            public Currency? getCurrencyById(string EmployeeId,string id)
            {
                var currency = currencyDAO.getCurrencyById(id);
                return currency;
            }

        }
}





