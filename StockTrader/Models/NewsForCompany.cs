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

        public Quote StockInfo { get; set; }
    }
}