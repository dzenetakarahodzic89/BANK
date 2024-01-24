using BANK.DAO;
using static System.Console;
using System.Collections.Generic;
namespace BANK;
class Program
{
    static void Main(string[] args)
    {

        var currency = new CurrencyDAO();
        currency.GetCurrencyById("BAM");

        var dao = new CurrencyDAO();
        dao.load();




        //Console.WriteLine("Hello, World!");
    }
}

