using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTraderMongoService.Entities
{
    /// <summary>
    /// Klasa służy do przechowywania informacji o firmie, ktorą użytkownik posiada w swoim portfelu akcyjnym
    /// </summary>
    public class Stocks : MongoEntity
    {
        public Stocks()
        {
            TransactionHistories = new List<TransactionHistory>();
        }
        public bool IsObserved { get; set; }
        public string CompanyName { get; set; }
        public string CompanySymbol { get; set; }
        public int NumberOfStocks { get; set; }

        public IList<TransactionHistory> TransactionHistories { get; set; }

    }
}
