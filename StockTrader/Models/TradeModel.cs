using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StockTrader.Models
{
    public class TradeModel
    {
        public string CompanySymbol { get; set; }
        public int StockNumber { get; set; }
        public string StockValue { get; set; }
    }
}