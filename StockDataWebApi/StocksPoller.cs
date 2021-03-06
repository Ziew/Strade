﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using StockDataWebApi.ApiRepository;

namespace StockDataWebApi
{
    /// <summary>
    /// Klasa do subskrypcji
    /// </summary>
    /// <param name="companyName">Symbol giełdowy firmy</param>
    public class StocksPoller : IStocksPoller
    {
        private IFinancialData _financialData;
        public IObservable<Quote> StockPriceChanges { get; private set; }



        public StocksPoller(IFinancialData financialData)
        {
            _financialData = financialData;
            Subscribe();
        }

        /// <summary>
        /// Metoda, która co 1 sekundę pobiera i uaktualnia finansowe dane 
        /// </summary>
        private void Subscribe()
        {
            StockPriceChanges = Observable.Defer(
                () => Observable.Return(_financialData.GetFinancialDataFromCompanies().quote.ToObservable())
).Sample(TimeSpan.FromSeconds(1)).Retry()
                    .Repeat()
                .SelectMany(observable => observable)
                .GroupBy(quote => quote.symbol)
                .Select(
                    observable => observable.Distinct(ProjectionEqualityComparer<Quote>.Create(qoute => qoute.LastTradePriceOnly)))
                .SelectMany(observable => observable)
                .Publish().RefCount();


        }


    }
    class StockPoller
    {
        static void Main(string[] args)
        {
            var financialData = new StocksPoller(new FinancialData());
            financialData.StockPriceChanges.Subscribe(quote => Console.WriteLine(quote.Symbol + " " + quote.LastTradePriceOnly));
            Console.ReadKey();

        }
    }
}
