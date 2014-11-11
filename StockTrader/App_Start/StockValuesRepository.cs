using Microsoft.AspNet.SignalR;
using StockDataWebApi;
using StockDataWebApi.ApiRepository;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace StockTrader.App_Start
{
    public class StockValuesRepository : IStockValuesRepository
    {
        public Dictionary<string, double> Companies;

        public double getValue(string companySymbol)
        {
            try
            {
                return Companies[companySymbol];
            }
            catch (Exception e)
            {
                return 0;
            }

        }

        public StockValuesRepository(IStocksPoller stocksPoller)
        {
            Companies = new Dictionary<string, double>();
            var financialData = stocksPoller;
            financialData.StockPriceChanges.Subscribe(quote => Companies[quote.symbol] = Double.Parse(quote.LastTradePriceOnly, CultureInfo.InvariantCulture));
            //financialData.StockPriceChanges.Subscribe(quote =>
            //{
            //     var hubContext = GlobalHost.ConnectionManager.GetHubContext<PricingHub>();
            //     if (hubContext != null)
            //     {
            //         var stats = quote.LastTradePriceOnly;
            //        hubContext.Clients.All.updateStatistics("test",stats);
            //     }
            //});
        }

    }
}