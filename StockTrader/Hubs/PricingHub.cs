using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using StockTrader.Hubs;

namespace StockTrader
{
        public static class ServiceConstants
    {
        public static class Server
        {
            public const string UsernameHeader = "User";

            // pricing
            public const string PricingHub = "PricingHub";
            public const string SubscribePriceStream = "SubscribePriceStream";
            public const string UnsubscribePriceStream = "UnsubscribePriceStream";

            // blotter
            public const string BlotterHub = "BlotterHub";
            public const string SubscribeTrades = "SubscribeTrades";
            public const string UnsubscribeTrades = "UnsubscribeTrades";

            // reference data
            public const string ReferenceDataHub = "ReferenceDataHub";
            public const string GetCurrencyPairs = "GetCurrencyPairs";

            // executution
            public const string ExecutionHub = "ExecutionHub";
            public const string Execute = "Execute";

            // control
            public const string ControlHub = "ControlHub";
            public const string SetPriceFeedThroughput = "SetPriceFeedThroughput";
            public const string GetPriceFeedThroughput = "GetPriceFeedThroughput";
            public const string GetCurrencyPairStates = "GetCurrencyPairStates";
            public const string SetCurrencyPairState = "SetCurrencyPairState";
        }

        public static class Client
        {
            public const string OnNewPrice = "OnNewPrice";
            public const string OnNewTrade = "OnNewTrade";
            public const string OnCurrencyPairUpdate = "OnCurrencyPairUpdate"; 
        }
    }
    [HubName(ServiceConstants.Server.PricingHub)]
    public class PricingHub : Hub
    {
        private IContextHolder _contextHolder;

        public PricingHub(IContextHolder contextHolder)
        {
            _contextHolder = contextHolder;
            
        }

        [HubMethodName(ServiceConstants.Server.SubscribePriceStream)]
        public async Task SubscribePriceStream()
        {
            _contextHolder.PricingHubClient = Clients;
        }

        [HubMethodName(ServiceConstants.Server.UnsubscribePriceStream)]
        public async Task UnsubscribePriceStream()
        {

        }
    }
}