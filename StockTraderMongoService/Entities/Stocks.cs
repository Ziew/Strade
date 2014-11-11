using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTraderMongoService.Entities
{
    public class Stocks : MongoEntity
    {
        
        public string CompanyName { get; set; }
        public string CompanySymbol { get; set; }
        public int NumberOfStocks { get; set; }
        public IList<TransactionHistory> TransactionHistories { get; set; }

    }
}
