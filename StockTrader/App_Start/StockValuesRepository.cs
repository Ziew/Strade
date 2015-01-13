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
    /// <summary>
    /// Klasa, która służy do pobierania i wysyłania finansowych danych 
    /// </summary>
    public class StockValuesRepository : IStockValuesRepository
    {
        public Dictionary<string, double> Companies;

        /// <summary>
        /// Metoda służąca do pobrania wartości akcji
        /// </summary>
        /// <param name="companySymbol">Symbol giełdowy firmy</param>
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

        /// <summary>
        /// Konstruktor klasy w którym subksrybujemy
        /// </summary>

        public StockValuesRepository(IStocksPoller stocksPoller)
        {
            Companies = new Dictionary<string, double>();
            var financialData = stocksPoller;
            financialData.StockPriceChanges.Subscribe(quote => Companies[quote.symbol] = Double.Parse(quote.LastTradePriceOnly));
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