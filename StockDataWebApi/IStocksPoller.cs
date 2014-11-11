using System;
using StockDataWebApi.ApiRepository;

namespace StockDataWebApi
{
    public interface IStocksPoller
    {
        IObservable<Quote> StockPriceChanges { get; }
    }
}