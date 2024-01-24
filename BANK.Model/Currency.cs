using System;
namespace BANK.Model
{
    public class Currency
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public double ExchangeRate { get; set; }
        public double ExchangeRateB { get; set; }
        public double ExchangeRateS { get; set; }

        public Currency(string id, string name, double exchangeRate, double exchangeRateB, double exchangeRateS)
        {
            Id = id;
            Name = name;
            ExchangeRate = exchangeRate;
            ExchangeRateB = exchangeRateB;
            ExchangeRateS = exchangeRateS;
        }
    }
}

