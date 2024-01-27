using System;
using Microsoft.Extensions.DependencyInjection;
using BANK.Model;

namespace BANK.DAO
{
    public interface ICurrencyDAO
    {


        public void save();
        public void load();
        public Currency? getCurrencyById(string id);
        public List<Currency> getAllCurrency();
        public Currency? removeById(string id);
        public Currency? createCurrency(string id, string name, double exchangeRate, double exchangeRateB, double exchangeRateS);
        public Currency? editCurrency(string id, string name, double exchangeRate, double exchangeRateB, double exchangeRateS);










    }
}

