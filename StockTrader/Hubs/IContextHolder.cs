using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Hubs;
using StockDataWebApi;
using StockTrader.App_Start;

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

        public PricePublisher(IContextHolder contextHolder, IStocksPoller stockPoller)
        {
            _contextHolder = contextHolder;
            stockPoller.StockPriceChanges.Subscribe(async quote =>
            {
                var context = _contextHolder.PricingHubClient;
                if (context == null) return;

                await context.Caller.OnNewPrice(new { symbol = quote.symbol, value = quote.LastTradePriceOnly});
            });
        }
    }
}
