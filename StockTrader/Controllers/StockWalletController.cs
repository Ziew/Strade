using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.IdGenerators;
using StockTrader.App_Start;
using StockTrader.Models;
using StockTraderMongoService.Entities;
using StockTraderMongoService.Services;
using System.Globalization;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity;
using StockDataWebApi.ApiRepository;
using Newtonsoft.Json.Linq;

namespace StockTrader.Controllers
{
    [Authorize]
    public class StockWalletController : Controller
    {
        private StockWalletService _stockWalletService;
        private IStockValuesRepository StockValuesRepository;
        private IFinancialData _financialData;

        public StockWalletController(IStockValuesRepository stockValuesRepository, IFinancialData financialData)
        {
            _stockWalletService = new StockWalletService();
            StockValuesRepository = stockValuesRepository;
            _financialData = financialData;
        }

        public ActionResult Index()
        {

           

            var user = _stockWalletService.GetByUser(User.Identity.Name);
            List<CompanyInfoForUserViewModel> stockwallet = new List<CompanyInfoForUserViewModel>();
            if (user != null)
            {
                stockwallet = user.OwnedStocks.Where(n => n.IsObserved).Select(n => new CompanyInfoForUserViewModel
                {
                    
                    CompanyName = n.CompanyName,
                    Company = GetNewsForCompany(n.CompanySymbol).Take(2).ToList(),
                    CompanySymbol = n.CompanySymbol,
                    StocksNumber = n.NumberOfStocks,
                    CurrentValue = StockValuesRepository.getValue(n.CompanySymbol),
                    TotalValue = n.TransactionHistories.Aggregate(0.0,(previous,current) =>
                    
                        previous + current.NumberOfStock*current.StockPrice
                       )

                }).ToList();
            }
            var allstockvaluebytransactionprice = stockwallet.Aggregate(0.0,(previous, current) => previous + current.TotalValue);
            var allstockvaluebycurrentprice = stockwallet.Aggregate(0.0,(previous, current) => previous + (current.StocksNumber * StockValuesRepository.getValue(current.CompanySymbol)));
            if (user == null)
            {
                var userstatistics = new UserStatisticsWithOwnedCompanyInfo
                {
                    UserMoney = 100000,
                    OwnedCompanyInfo = new List<CompanyInfoForUserViewModel>(),

                    AllStockValue = 0,
                    Income = 0,
                    AllValue = 100000

                };

                return View(userstatistics);
            }
            else
            {

                var userstatistics = new UserStatisticsWithOwnedCompanyInfo
                {
                    UserMoney = user.Money,
                    OwnedCompanyInfo = stockwallet,

                    AllStockValue = allstockvaluebycurrentprice,
                    Income = allstockvaluebycurrentprice - allstockvaluebytransactionprice,
                    AllValue = allstockvaluebycurrentprice + user.Money

                };

                return View(userstatistics);
            }
        }

        private List<NewsForCompanyWithoutStockInfo> GetNewsForCompany(string companySymbol)
        {
            dynamic jsonobject = JObject.Parse(_financialData.GetNewsForCompany(companySymbol));
            var stockinfo = _financialData.GetFinancialDataFromCompanies();
            var list = new List<NewsForCompanyWithoutStockInfo>();

            foreach (var d in jsonobject.rss.channel.item)
            {

                list.Add(new NewsForCompanyWithoutStockInfo
                {
                    Description = d.description,
                    Header = d.title,
                    Link = d.link,
                    //StockInfo = stockinfo.quote.First(n => n.symbol == companySymbol),
                    PubDate = d.pubDate
                });
            }
            return list;
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CompanyInfoForUserViewModel companyInfoForUserViewModel)
        {
            var user = _stockWalletService.GetByUser(User.Identity.Name);
            
            var stock = new Stocks
            {
                CompanyName = companyInfoForUserViewModel.CompanySymbol,
                CompanySymbol = companyInfoForUserViewModel.CompanySymbol,
                NumberOfStocks = 0,
                IsObserved = true
            };
            if (user == null)
            {
                _stockWalletService.Create(new StockWallet
                {
                    UserEmail = User.Identity.Name,
                    Money = 100000,
                    OwnedStocks = new List<Stocks>
                    {
                        stock
                    }

                });
            }
            else
            {
                _stockWalletService.AddStock(User.Identity.Name, stock);
            }

            return Json(null, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult Buy(string companySymbol )
        {

            return PartialView(new TradeModel { CompanySymbol = companySymbol });
        }

        
        [HttpGet]
        public ActionResult Delete(string companySymbol)
        {

            return PartialView(new TradeModel { CompanySymbol = companySymbol });
        }

   

        [HttpPost]
        public ActionResult Delete(TradeModel tradeModel)
        {
   
                _stockWalletService.DeleteStock(User.Identity.Name,tradeModel.CompanySymbol);

                return Json(new { Result = "Success" });
    
        }
      

        [HttpGet]
        public ActionResult Sell(string companySymbol)
        {

            return PartialView(new TradeModel { CompanySymbol = companySymbol });
        }

       



        [HttpPost]
        public ActionResult Buy(TradeModel tradeModel)
        {
            var user = _stockWalletService.GetByUser(User.Identity.Name);
           
            var stock = new TransactionHistory
            {
                NumberOfStock = tradeModel.StockNumber,
                TransactionDate = DateTime.Now,
                StockPrice = StockValuesRepository.getValue(tradeModel.CompanySymbol)
            };
            if (user.Money >= stock.StockPrice * stock.NumberOfStock)
            {

                _stockWalletService.AddTransaction(User.Identity.Name, tradeModel.CompanySymbol, tradeModel.StockNumber, stock);


                return Json(new { Result = "Success" });
            }
            return Json(new { Result = "Failed", Transaction = "Buy" });
        }
        public ActionResult Sell(TradeModel tradeModel)
        {
            var user = _stockWalletService.GetByUser(User.Identity.Name).OwnedStocks.FirstOrDefault(n => n.CompanySymbol == tradeModel.CompanySymbol);
            var stock = new TransactionHistory
            {
                NumberOfStock = -1*tradeModel.StockNumber,
                TransactionDate = DateTime.Now,
                StockPrice = StockValuesRepository.getValue(tradeModel.CompanySymbol)
            };

            if (user.NumberOfStocks - tradeModel.StockNumber > 0)
            {

                _stockWalletService.AddTransaction(User.Identity.Name, tradeModel.CompanySymbol, (-1) * tradeModel.StockNumber, stock);


                return Json(new { Result = "Success" });
            }
            return Json(new { Result = "Failed", Transaction ="Sell" });
        }
    }
}