﻿using BANK.DAO;
using BANK.Model;
using BANK.Services;
using static System.Console;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using static System.Net.Mime.MediaTypeNames;

namespace BANK;
class Program
{
    // pomocna metoda, izbaciti kasnije 
    public static void printCurrencies(List<Currency> currencies)
    {

        foreach (var p in currencies)
        {
            Console.WriteLine($"{p.Id} - {p.Name} - {p.ExchangeRate} - {p.ExchangeRateB} - {p.ExchangeRateS}");
        }

    }


    static void Main(string[] args)
    {


        var services = CreateServices();

        var currenciesDAO = services.GetRequiredService<ICurrencyDAO>();

        var peopleDAO = services.GetRequiredService<IPersonDAO>();

        currenciesDAO.load();
        peopleDAO.save();
        peopleDAO.load();

        var currenciesServices = services.GetRequiredService<ICurrencyService>();

        var listCurrency = currenciesServices.getAllCurrencies();
        printCurrencies(listCurrency);






        //var dao = new CurrencyDAO();
        //dao.load();
        ////var currency = dao.getCurrencyById("BAM");
        ////WriteLine(currency?.Name);

        //var currencies = dao.getAllCurrency();
        //printCurrencies(currencies);
        //Console.WriteLine("nova valuta");

        //dao.createCurrency("HKNH", "MDSK", 5.67, 6.78, 23.45);
        //var currencies2 = dao.getAllCurrency();
        //printCurrencies(currencies);

        //Console.WriteLine($"Brisanje valute BAM ");
        //dao.removeById("BAM");
        //printCurrencies(dao.getAllCurrency());

    }

    // Registracija DI
    private static ServiceProvider CreateServices()
    {
        var serviceCollection = new ServiceCollection();

        // Registracija DAO kao singleton
        serviceCollection.AddSingleton<ICurrencyDAO, CurrencyDAO>();
        serviceCollection.AddSingleton<IPersonDAO, PersonDAO>();

        // Registracija servisa, DI će automatski proslijediti CurrencyDAO instancu
        serviceCollection.AddSingleton<ICurrencyService, CurrencyService>();

        // Izgradnja providera servisa
        var serviceProvider = serviceCollection.BuildServiceProvider();

        return serviceProvider;
    }






}



