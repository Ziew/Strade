using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockTraderMongoService.Entities;

namespace StockTraderMongoService.Services
{
    public class TransactionHistoryService : EntityService<TransactionHistory>
    {
        public override void Update(TransactionHistory entity)
        {
            throw new NotImplementedException();
        }
    }
}
