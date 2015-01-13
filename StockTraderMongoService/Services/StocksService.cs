using MongoDB.Driver;
using MongoDB.Driver.Builders;
using StockTraderMongoService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTraderMongoService.Services
{
    /// <summary>
    /// Serwis dla akcji firm
    /// </summary>
    public class StocksService : EntityService<Stocks>
    {
        public override void Update(Stocks entity)
        {
            throw new NotImplementedException();
        }

       
    }
}
