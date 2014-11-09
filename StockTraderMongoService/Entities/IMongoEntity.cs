using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTraderMongoService.Entities
{
  
    public interface IMongoEntity
    {
        ObjectId Id { get; set; }
    }
}
