using System;
using BANK.Model;
using Microsoft.Extensions.DependencyInjection;
namespace BANK.DAO

{
    public interface ICurrencyService
    {
        public List<Currency> getAllCurrencies();
        public Currency? createCurrency(string id, string name, double exchangeRate, double exchangeRateB, double exchangeRateS);
        public Currency? editCurrency(string userId, string id, string name, double exchangeRate, double exchangeRateB, double exchangeRateS);
        public Currency? removeById(string id);
        public Currency? getCurrencyById(string id);
    }
}

