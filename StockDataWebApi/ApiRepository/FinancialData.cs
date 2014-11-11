using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace StockDataWebApi.ApiRepository
{


    public class Quote
    {
        public string symbol { get; set; }
        public string AverageDailyVolume { get; set; }
        public string Change { get; set; }
        public string DaysLow { get; set; }
        public string DaysHigh { get; set; }
        public string YearLow { get; set; }
        public string YearHigh { get; set; }
        public string MarketCapitalization { get; set; }
        public string LastTradePriceOnly { get; set; }
        public string DaysRange { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public string Volume { get; set; }
        public string StockExchange { get; set; }
    }

    public class Results
    {
        public List<Quote> quote { get; set; }
    }

    public class FinancialData : IFinancialData
    {
        public Results GetFinancialDataFromCompany(String companyName)
        {
            string results = "";
            using (WebClient wc = new WebClient())
            {
                results = wc.DownloadString(@"https://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20yahoo.finance.quote%20where%20symbol%20in%20(%22YHOO%22%2C%22AAPL%22%2C%22GOOG%22%2C%22MSFT%22)&format=json&diagnostics=true&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys&callback=");
            }


            var users = JObject.Parse(results).SelectToken("query").SelectToken("results").ToString();
            return JsonConvert.DeserializeObject<Results>(users);
        }

        public Results GetFinancialDataFromCompanies()
        {
            var results = "";
            using (var wc = new WebClient())
            {
                results = wc.DownloadString(@"http://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20yahoo.finance.quote%20where%20symbol%20in%20(%22YHOO%22%2C%22AAPL%22%2C%22GOOG%22%2C%22MSFT%22)&format=json&diagnostics=true&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys&callback=");
            }

            var users = JObject.Parse(results).SelectToken("query").SelectToken("results").ToString();
            var d = JsonConvert.DeserializeObject<Results>(users);
            try
            {
                foreach (var v in d.quote)
                {
                    v.LastTradePriceOnly =
                          (new Random().NextDouble() + Double.Parse(v.LastTradePriceOnly, CultureInfo.InvariantCulture))
                             .ToString();
                }
            }
            catch (Exception e) { }
            

            return d;
        }


    }
}
