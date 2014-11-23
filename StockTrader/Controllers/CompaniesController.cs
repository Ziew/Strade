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

        public CompaniesController(IEntityService<Company> entityService, IFinancialData financialData)
        {
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

        public ActionResult GetNewsForCompany(string companyName)
        {
            dynamic jsonobject = JObject.Parse(_financialData.GetNewsForCompany(companyName));

            var list = new List<NewsForCompany>();

            foreach (var d in jsonobject.rss.channel.item)
            {
                list.Add(new NewsForCompany
                {
                    Description = d.description,
                    Header = d.title,
                    Link = d.link
                });
            }

            return Json(list, JsonRequestBehavior.AllowGet);
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