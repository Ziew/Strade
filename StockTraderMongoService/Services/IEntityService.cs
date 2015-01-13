using StockTraderMongoService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTraderMongoService.Services
{
    /// <summary>
    /// Interfejsy do serwisów
    /// </summary>
    public interface IEntityService<T> where T : IMongoEntity
    {
     
        void Create(T entity);

        void Delete(string id);

        T GetById(string id);

        IEnumerable<T> GetAll();

        void Update(T entity);
    }
}
