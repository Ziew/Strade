using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockTraderMongoService.Entities;

namespace StockTraderMongoService.Entities
{
    public class TransactionHistory : MongoEntity
    {
        public string UserEmail { get; set; }

        public Company CompanyStocks { get; set; }

        public int NumberOfStock { get; set; }

        public DateTime TransactionDate { get; set; }
    }
}
