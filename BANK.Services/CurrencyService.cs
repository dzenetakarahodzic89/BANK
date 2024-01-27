namespace BANK.Services;

using System.Collections.Generic;
using BANK.DAO;
using BANK.Model;
using Microsoft.Extensions.DependencyInjection;


public class CurrencyService : ICurrencyService

{

    // zato sto servis radi s DAO-M , definise ga kao atribut klase 
    private readonly ICurrencyDAO currencyDAO;

    // konstruktor
    public CurrencyService(ICurrencyDAO currencyDAO)

    {
        this.currencyDAO = currencyDAO; // atribut klase pripada klasi sa this.
    }

    public List<Currency> getAllCurrencies()
    {
        return currencyDAO.getAllCurrency();
    }

    public Currency? createCurrency(string id, string name, double exchangeRate, double exchangeRateB, double exchangeRateS)
    {

        //pozivanje metode iz currencyDAO

        // prije upisa u bazu moram da provjerim da su podaci za kreiranje koji su uneseni ispravni

        if (id == null || id.Equals("") || name == null || name.Equals("") || name.Length < 5
            || exchangeRate <= 0 || exchangeRateB <= 0 || exchangeRateS <= 0)

        {
            throw new Exception("Currency data is not valid! Please try again.");
        }

        var currency = currencyDAO.createCurrency(id, name, exchangeRate, exchangeRateB, exchangeRateS);
        return currency;

        //koristi DAO za pristup i manipulaciju podacima, ali dodaje dodatni sloj obrade.TO JE OVO GORE 
    }

    // TODO: trebam da provjerim ko vrsi izmjenu , a to je administrator uposlenik banke

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


    public Currency? removeById(string id)
    {
        //TODO: dodati uslove za provjeru

        var currency = currencyDAO.removeById(id);
        return currency;
    }
    public Currency? getCurrencyById(string id)
    {
        var currency = currencyDAO.getCurrencyById(id);
        return currency;
    }

}








