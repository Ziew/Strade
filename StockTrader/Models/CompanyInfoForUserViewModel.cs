using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StockTrader.Models
{
    public class CompanyInfoForUserViewModel
    {
        public string CompanyName { get; set; }
        public string CompanySymbol { get; set; }
        public int StocksNumber { get; set; }
        public double CurrentValue { get; set; }

        public string TotalValue { get; set; }


    }
}