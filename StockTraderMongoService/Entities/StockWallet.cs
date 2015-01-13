using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Serializers;

namespace StockTraderMongoService.Entities
{
    /// <summary>
    /// Klasa służy do przechowywania portfela aukcyjnego użytkownika
    /// </summary>
    public class StockWallet : MongoEntity
    {
      
        public StockWallet()
        {
            OwnedStocks = new List<Stocks>();
        }
        public string UserEmail { get; set; }

        public double Money { get; set; }
    
        public IList<Stocks> OwnedStocks { get; set; }
    }
}
