using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StockDataWebApi.ApiRepository;

namespace StockTrader.Models
{
    public class NewsForCompany
    {
        public string Header { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }

        public string PubDate { get; set; }
       
        public Quote StockInfo { get; set; }

        //public List<Image> Charts { get; set; }
    }

    public class NewsForCompanies
    {
        public string CompanyName { get; set; }
        public string CompanySymbol { get; set; }

        public List<NewsForCompany> Company { get; set; }
    }

    public class NewsForCompanyWithoutStockInfo
    {
        public string Header { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }

        public string PubDate { get; set; }

    

        //public List<Image> Charts { get; set; }
    }
}