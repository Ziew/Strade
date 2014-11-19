using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Hubs;
using StockDataWebApi;
using StockTrader.App_Start;
using StockTraderMongoService.Services;

namespace StockTrader.Hubs
{
    public interface IContextHolder
    {
        IHubCallerConnectionContext<dynamic> PricingHubClient { get; set; }
    }

    public class ContextHolder : IContextHolder
    {
        public IHubCallerConnectionContext<dynamic> PricingHubClient { get; set; }
    }

    public class PricePublisher
    {
        private readonly IContextHolder _contextHolder;
        private StockWalletService _stockWalletService;

        public PricePublisher(IContextHolder contextHolder, IStocksPoller stockPoller)
        {
            _stockWalletService = new StockWalletService();
            _contextHolder = contextHolder;
            stockPoller.StockPriceChanges.Subscribe(async quote =>
            {
                var context = _contextHolder.PricingHubClient;
                if (context == null) return;
                double total = 0;
           
                await context.Caller.OnNewPrice(new { symbol = quote.symbol, value = quote.LastTradePriceOnly, });
            });
        }
    }
}
