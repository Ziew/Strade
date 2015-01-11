using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using StockDataWebApi.ApiRepository;
using StockTrader.Models;
using StockTraderMongoService.Entities;
using StockTraderMongoService.Services;
using Newtonsoft.Json.Linq;

namespace StockTrader.Controllers
{
    public class CompaniesController : Controller
    {
        private readonly IEntityService<Company> _entityService;
        private IFinancialData _financialData;
        private StockWalletService _stockWalletService;

        public CompaniesController(IEntityService<Company> entityService, IFinancialData financialData)
        {
            _stockWalletService = new StockWalletService();
            _entityService = entityService;
            _financialData = financialData;
        }

        public ActionResult Index()
        {
            var financialData = _financialData.GetFinancialDataFromCompanies();
            var companies =
                financialData.quote.Select(n => new CompanyViewModel { name = n.Name, symbol = n.symbol});
            return View(companies);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult GetNewsForCompany(string companySymbol)
        {
            dynamic jsonobject = JObject.Parse(_financialData.GetNewsForCompany(companySymbol));
            var stockinfo = _financialData.GetFinancialDataFromCompanies();
            var list = new List<NewsForCompany>();

            foreach (var d in jsonobject.rss.channel.item)
            {

                list.Add(new NewsForCompany
                {
                    Description = d.description,
                    Header = d.title,
                    Link = d.link,
                  
                    PubDate = d.pubDate
                });
            }
            var stockinfo2 = stockinfo.quote.FirstOrDefault(n => n.symbol == companySymbol);
            var companies = new NewsForCompanies
            {
                Change = stockinfo2.Change,
                DaysHigh = stockinfo2.DaysHigh,
                DaysLow = stockinfo2.DaysLow,
                LastTradePriceOnly = stockinfo2.LastTradePriceOnly,
                MarketCapitalization = stockinfo2.MarketCapitalization,
                Volume = stockinfo2.Volume,
                Company = list,
                CompanySymbol = companySymbol,
                IsObserve = _stockWalletService.GetByUser(User.Identity.Name).OwnedStocks.FirstOrDefault(n => n.CompanySymbol == companySymbol) != null
            };
            return PartialView(companies);
            //return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Create(CompanyViewModel company)
        {
            _entityService.Create(new Company
            {
                CompanyName = company.name
            });
            return Json(new { Result = "Success" });
        }
    }
}