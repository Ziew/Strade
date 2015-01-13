using StockTraderMongoService.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTraderMongoService.Services
{

    /// <summary>
    /// klasa abstrakcyjna wykorzystywana do tworzenia serwisów dla klas bazo danowych
    /// </summary>
    public abstract class EntityService<T> : IEntityService<T> where T : IMongoEntity
    {
        protected readonly MongoConnectionHandler<T> MongoConnectionHandler;
        /// <summary>
        /// Metoda służąca do dodawania nowego obiektu do bazy
        /// </summary>
        /// <param name="entity">Obiekt entity</param>

        public virtual void Create(T entity)
        {
            //// Save the entity with safe mode (WriteConcern.Acknowledged)
            var result = this.MongoConnectionHandler.MongoCollection.Save(
                entity,
                new MongoInsertOptions
                {
                    WriteConcern = WriteConcern.Acknowledged
                });

            if (!result.Ok)
            {
                //// Something went wrong
            }
        }

        /// <summary>
        /// Metoda służąca do usuwania wpisu z bazy
        /// </summary>
        /// <param name="id">Id obiektu, który chcemy usunąć</param>

        public virtual void Delete(string id)
        {
            var result = this.MongoConnectionHandler.MongoCollection.Remove(
                Query<T>.EQ(e => e.Id,
                new ObjectId(id)),
                RemoveFlags.None,
                WriteConcern.Acknowledged);

            if (!result.Ok)
            {
                //// Something went wrong
            }
        }
        /// <summary>
        /// Konstruktor w którym tworzymy połączenie z bazą
        /// </summary>

        protected EntityService()
        {
            MongoConnectionHandler = new MongoConnectionHandler<T>();
        }




        /// <summary>
        /// Metoda służąca do pobrania obiektu z bazy
        /// </summary>
        /// <param name="id">Id obiektu, który chcemy pobrać</param>
        public virtual T GetById(string id)
        {
            var entityQuery = Query<T>.EQ(e => e.Id, new ObjectId(id));
            return this.MongoConnectionHandler.MongoCollection.FindOne(entityQuery);
        }


        /// <summary>
        /// Metoda służąca do pobrania wszystkich obiektów danego typu z bazy
        /// </summary>
        public virtual IEnumerable<T> GetAll()
        {
            return this.MongoConnectionHandler.MongoCollection.FindAll();
        }

        public abstract void Update(T entity);
    }
}
