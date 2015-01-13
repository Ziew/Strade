using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StockDataWebApi.ApiRepository;

namespace StockTrader.Models
{
    /// <summary>
    /// Klasa, która służy do przesyłania informacji o artykułach 
    /// </summary>
    public class NewsForCompany
    {
        public string Header { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }

        public string PubDate { get; set; }
       
   

        //public List<Image> Charts { get; set; }
    }
    /// <summary>
    /// Klasa, która służy do przesyłania informacji o danych giełdowych firmy oraz o nowościach 
    /// </summary>
    public class NewsForCompanies
    {
        public string CompanyName { get; set; }
        public string CompanySymbol { get; set; }
        public string Change { get; set; }
        public string DaysLow { get; set; }
        public string DaysHigh { get; set; }
        public string MarketCapitalization { get; set; }
        public string LastTradePriceOnly { get; set; }

        public bool IsObserve { get; set; }
        public string Volume { get; set; }

        public List<NewsForCompany> Company { get; set; }
    }

    /// <summary>
    /// Klasa, która służy do przesyłania informacji o artykułach 
    /// </summary>
    public class NewsForCompanyWithoutStockInfo
    {
        public string Header { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }

        public string PubDate { get; set; }

    

        //public List<Image> Charts { get; set; }
    }
}