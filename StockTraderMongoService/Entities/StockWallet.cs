using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTraderMongoService.Entities
{
    public class StockWallet : MongoEntity
    {
        public string UserEmail { get; set; }
        public IList<Stocks> OwnedStocks { get; set; }
    }
}
