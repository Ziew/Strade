using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockTraderMongoService.Entities;

namespace StockTraderMongoService.Entities
{  
    /// <summary>
    /// Klasa służy do przechowywania transakcji jakie użytkownik wykonał
    /// </summary>
    public class TransactionHistory : MongoEntity
    {


        public double StockPrice { get; set; }

        public int NumberOfStock { get; set; }

        public DateTime TransactionDate { get; set; }
    }
}
