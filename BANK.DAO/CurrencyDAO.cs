﻿using BANK.Model;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace BANK.DAO
{
    public class CurrencyDAO
    {
        List<Currency> currencies { get; set; } = new();




        /* *****************************************************************************************************************/

        public void save()

        {

            var currencies = new List<Currency>
            {
                new Currency("USD", "US Dollar", 1.00, 0.95, 1.05),
                new Currency("EUR", "Euro", 1.13, 1.10, 1.16),
                new Currency("BAM","Bosnian Convertible Marka", 1, 0.550, 0.560),
                new Currency("TRY", "Turkish Lira", 0.12, 0.11, 0.13),
                new Currency("SEK", "Swedish Krona", 0.11, 0.10, 0.12),
                new Currency("GBP", "British Pound", 1.35, 1.30, 1.40),
                new Currency("SAR", "Saudi Riyal", 0.27, 0.26, 0.28)
            };

            string path = Path.Combine(Environment.CurrentDirectory, "currencies.csv");

            Console.WriteLine($"{path}");

            using (var writer = new StreamWriter(path))
            {
                writer.WriteLine("Id,Name,ExchangeRate,ExchangeRateB,ExchangeRateS");

                foreach (var currency in currencies)
                {
                    writer.WriteLine($"\"{currency.Id}\",\"{currency.Name}\",{currency.ExchangeRate},{currency.ExchangeRateB},{currency.ExchangeRateS}");
                }
            }
        }






        /* *****************************************************************************************************************/

        public void load()
        {


            string path = Path.Combine(Environment.CurrentDirectory, "currencies.csv");

            if (!File.Exists(path))
            {
                Console.WriteLine("Fajl nije pronađen.");
                return;
            }

            currencies = new List<Currency>();

            using (var reader = new StreamReader(path))
            {
                reader.ReadLine();

                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    var values = line.Split(',');

                    if (values.Length == 5)
                    {
                        var currency = new Currency(
                            values[0].Trim('"'),
                            values[1].Trim('"'),
                            Convert.ToDouble(values[2]),
                            Convert.ToDouble(values[3]),
                            Convert.ToDouble(values[4])
                        );

                        currencies.Add(currency);
                    }
                }
            }


            //foreach (var currency in currencies)
            //{
            //    Console.WriteLine($"{currency.Id} - {currency.Name} - {currency.ExchangeRate}");
            //}


        }


        /* *****************************************************************************************************************/

        public Currency? getCurrencyById(string id)
        {   // Where metoda uzme element iz liste 
            return currencies.Where(currency => currency.Id.Equals(id)).FirstOrDefault();
        }


        /* *****************************************************************************************************************/

        public List<Currency> getAllCurrency()
        {
            return currencies;
        }



        /* *****************************************************************************************************************/

        public Currency? removeById(string id)
        {
            // Pronađi valutu u listi koja ima traženi ID
            var currencyToRemove = currencies.Find(currency => currency.Id.Equals(id));

            // Ako valuta postoji, ukloni je iz liste
            if (currencyToRemove != null)
            {
                currencies.Remove(currencyToRemove);
            }

            // Vrati uklonjenu valutu ili null ako valuta nije pronađena
            return currencyToRemove;
        }

        /* *****************************************************************************************************************/

        public Currency? createCurrency(string id, string name, double exchangeRate, double exchangeRateB, double exchangeRateS)
        {

            Currency newCurrency = new Currency(id, name, exchangeRate, exchangeRateB, exchangeRateS);

            return newCurrency;
        }


        /* *****************************************************************************************************************/




        public Currency? editCurrency(string id, string name, double exchangeRate, double exchangeRateB, double exchangeRateS)
        {

            var currencyToEdit = currencies.Find(currency => currency.Id.Equals(id));

            if (currencyToEdit != null)
            {
                currencyToEdit.Name = name;
                currencyToEdit.Id = id;
                currencyToEdit.ExchangeRate = exchangeRate;
                currencyToEdit.ExchangeRateB = exchangeRateB;
                currencyToEdit.ExchangeRateS = exchangeRateS;

                return currencyToEdit;
            }

            // Vrati izmjnenjenu valutu ili null ako valuta nije pronađena
            return null;

        }


        /* *****************************************************************************************************************/

    }





}

