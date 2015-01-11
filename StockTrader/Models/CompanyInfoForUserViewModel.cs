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

        public double TotalValue { get; set; }

        public List<NewsForCompanyWithoutStockInfo> Company { get; set; }

    }

    public class UserStatisticsWithOwnedCompanyInfo
    {
        public double AllStockValue { get; set; }
        public double UserMoney { get; set; }
        public double Income { get; set; }
        public double AllValue { get; set; }
            public List<CompanyInfoForUserViewModel> OwnedCompanyInfo {get; set;}
    }

}