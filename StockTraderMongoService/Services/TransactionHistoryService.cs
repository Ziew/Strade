using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockTraderMongoService.Entities;

namespace StockTraderMongoService.Services
{


    /// <summary>
    /// Serwis dla historii transakcji
    /// </summary>

    public class TransactionHistoryService : EntityService<TransactionHistory>
    {
        public override void Update(TransactionHistory entity)
        {
            throw new NotImplementedException();
        }
    }
}
