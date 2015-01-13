using StockTraderMongoService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTraderMongoService.Services
{
    /// <summary>
    /// Serwis do zarządzania obiektami klasy Company w bazie danych
    /// </summary>
    public class CompanyService : EntityService<Company>
    {

        public override void Update(Company entity)
        {
            throw new NotImplementedException();
        }

      
    }
}
