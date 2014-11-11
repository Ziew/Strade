using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StockTrader.Models
{
    public class StockWalletViewModel
    {
        public IList<CompanyInfoForUserViewModel> Companies { get; set; } 
    }
}