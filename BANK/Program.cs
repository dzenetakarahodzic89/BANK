using BANK.DAO;
using static System.Console;
using System.Collections.Generic;
namespace BANK;
class Program
{
    static void Main(string[] args)
    {




        var dao = new CurrencyDAO();
        dao.load();
        var currency = dao.getCurrencyById("BAM");
        WriteLine(currency?.Name);


        //Console.WriteLine("Hello, World!");
    }
}

