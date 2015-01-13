using StockTraderMongoService.Entities;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTraderMongoService.Entities
{
    /// <summary>
    /// Klasa
    /// </summary>
      [BsonIgnoreExtraElements]
      public class Company : MongoEntity
    {
        public string Symbol { get; set; }
        public string CompanyName { get; set; }

        public double StockPrice { get; set; }
    }
}
