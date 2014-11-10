using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using StockTrader.Models;
using StockTraderMongoService.Entities;
using StockTraderMongoService.Services;

namespace StockTrader.Controllers
{
    public class CompaniesController : Controller
    {
        private readonly IEntityService<Company> _entityService;


        public CompaniesController(IEntityService<Company> entityService)
        {
            _entityService = entityService;
        }

        public ActionResult Index()
        {
            IEnumerable<CompanyViewModel> companies = _entityService.GetAll().Select(n => new CompanyViewModel
            {
                name = n.CompanyName,
                price = n.ActionPrice
            });
            return View(companies);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CompanyViewModel company)
        {
            _entityService.Create(new Company
            {
                ActionPrice = company.price,
                CompanyName = company.name
            });
            return Json(new {Result = "Success"});
        }
    }
}