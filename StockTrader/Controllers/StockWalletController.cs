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

namespace StockTrader.Controllers
{
    [Authorize]
    public class StockWalletController : Controller
    {
        private StockWalletService _stockWalletService;
        private IStockValuesRepository StockValuesRepository;

        public StockWalletController(IStockValuesRepository stockValuesRepository)
        {
            _stockWalletService = new StockWalletService();
            StockValuesRepository = stockValuesRepository;
        }

        public ActionResult Index()
        {



            var user = _stockWalletService.GetByUser(User.Identity.Name);
            List<CompanyInfoForUserViewModel> stockwallet = new List<CompanyInfoForUserViewModel>();
            if (user != null)
            {
                stockwallet = user.OwnedStocks.Select(n => new CompanyInfoForUserViewModel
                {
                    CompanyName = n.CompanyName,
                    CompanySymbol = n.CompanySymbol,
                    StocksNumber = n.NumberOfStocks,
                    CurrentValue = StockValuesRepository.getValue(n.CompanySymbol),
                    TotalValue = n.TransactionHistories.Aggregate(0.0,(previous,current) =>
                    
                        previous + current.NumberOfStock*current.StockPrice
                       ).ToString()

                }).ToList();
            }

            return View(stockwallet);
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
                NumberOfStocks = 0
            };
            if (user == null)
            {
                _stockWalletService.Create(new StockWallet
                {
                    UserEmail = User.Identity.Name,
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
        public ActionResult Sell(string companySymbol)
        {

            return PartialView(new TradeModel { CompanySymbol = companySymbol });
        }




        [HttpPost]
        public ActionResult Buy(TradeModel tradeModel)
        {
           
            var stock = new TransactionHistory
            {
                NumberOfStock = tradeModel.StockNumber,
                TransactionDate = DateTime.Now,
                StockPrice = StockValuesRepository.getValue(tradeModel.CompanySymbol)
            };


            _stockWalletService.AddTransaction(User.Identity.Name, tradeModel.CompanySymbol, tradeModel.StockNumber, stock);
            

            return Json(new { Result = "Success" });
        }
        public ActionResult Sell(TradeModel tradeModel)
        {

            var stock = new TransactionHistory
            {
                NumberOfStock = -1*tradeModel.StockNumber,
                TransactionDate = DateTime.Now,
                StockPrice = StockValuesRepository.getValue(tradeModel.CompanySymbol)
            };


            _stockWalletService.AddTransaction(User.Identity.Name, tradeModel.CompanySymbol, (-1) * tradeModel.StockNumber, stock);


            return Json(new { Result = "Success" });
        }
    }
}