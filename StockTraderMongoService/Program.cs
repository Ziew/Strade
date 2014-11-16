using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockTraderMongoService.Entities;
using StockTraderMongoService.Services;

namespace StockTraderMongoService
{
    class Program
    {
        static void Main(string[] args)
        {
            StockWalletService s = new StockWalletService();
            var d = new TransactionHistory
            {
                NumberOfStock = 55,
                StockPrice = 22.33,
                TransactionDate = DateTime.Now
            };
            s.AddTransaction("asdqwe1@tlen.pl","GOOG", 555, d);
        }
    }
}
