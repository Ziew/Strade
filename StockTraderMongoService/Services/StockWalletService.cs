using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver.Linq;
using StockTraderMongoService.Entities;
using MongoDB.Bson;
using MongoDB.Driver.Builders;
using MongoDB.Driver;

namespace StockTraderMongoService.Services
{

    /// <summary>
    /// Serwis dla portfela akcyjnego
    /// </summary>

    public class StockWalletService : EntityService<StockWallet>
    {
        public override void Update(StockWallet entity)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Metoda służąca do pobrania obiektu z bazy po użytkowniku
        /// </summary>
        /// <param name="userEmail">Email użytkownika</param>
        public virtual StockWallet GetByUser(string userEmail)
        {
            var entityQuery = Query<StockWallet>.EQ(e => e.UserEmail, userEmail);
            return MongoConnectionHandler.MongoCollection.FindOne(entityQuery);
        }

        /// <summary>
        /// Metoda służąca do usunięcia z obserwowanych firmy z portfela akcyjnego
        /// </summary>
        /// <param name="userId">Email użytkownika</param>
        /// <param name="companySymbol">symbol giełdowy firmy</param>

        public void DeleteStock(string userId, string companySymbol)
        {
            var updateResult =
                MongoConnectionHandler.MongoCollection.AsQueryable<StockWallet>()
                    .Where(p => p.UserEmail == userId);

            var v = updateResult.FirstOrDefault();

            var w = v.OwnedStocks.FirstOrDefault(p => p.CompanySymbol == companySymbol);
            w.IsObserved = false;


            MongoConnectionHandler.MongoCollection.Save(v);
        }

        /// <summary>
        /// Metoda służąca do dodania do obserwowanych firmy w portfelu akcyjnym
        /// </summary>
        /// <param name="userId">Email użytkownika</param>
        /// <param name="stock">obiekt z informacji o firmie</param>
        public void AddStock(string userId, Stocks stock)
        {
            var updateResult =
              MongoConnectionHandler.MongoCollection.AsQueryable<StockWallet>()
                  .Where(p => p.UserEmail == userId);

            var v = updateResult.FirstOrDefault();

            var w = v.OwnedStocks.FirstOrDefault(p => p.CompanySymbol == stock.CompanySymbol);
            if (w != null)
            {
                w.IsObserved = true;
                MongoConnectionHandler.MongoCollection.Save(v);
            }
            else
            {
                MongoConnectionHandler.MongoCollection.Update(
                         Query<StockWallet>.EQ(p => p.UserEmail, userId),
                         Update<StockWallet>.Push(p => p.OwnedStocks, stock),
                         new MongoUpdateOptions
                         {
                             WriteConcern = WriteConcern.Acknowledged
                         });
            }

        }
        /// <summary>
        /// Metoda służąca do dodania nowej transakcji
        /// </summary>
        /// <param name="userId">Email użytkownika</param>
        /// <param name="companySymbol">Symbol giełdowy firmy</param>
        /// <param name="stockNumber">Liczba akcji</param>
        /// <param name="transactionHistory">Informacje o transakcji</param>
        public void AddTransaction(string userId, string companySymbol, int stockNumber, TransactionHistory transactionHistory)
        {

            var updateResult =
                MongoConnectionHandler.MongoCollection.AsQueryable<StockWallet>()
                    .Where(p => p.UserEmail == userId);

            var v = updateResult.FirstOrDefault();
            v.Money -= stockNumber * transactionHistory.StockPrice;
            var w = v.OwnedStocks.FirstOrDefault(p => p.CompanySymbol == companySymbol);

            w.TransactionHistories.Add(transactionHistory);
            w.NumberOfStocks += stockNumber;
            MongoConnectionHandler.MongoCollection.Save(v);
            //Update(
            //        Query<StockWallet>.EQ(p => p.UserEmail, userId),
            //        Update<StockWallet>.Push(p => p.OwnedStocks.ToList().First(w => w.CompanySymbol == companySymbol).TransactionHistories, transactionHistory),
            //        new MongoUpdateOptions
            //        {
            //            WriteConcern = WriteConcern.Acknowledged
            //        });

            //if (updateResult.DocumentsAffected == 0)
            //{
            //    //// Something went wrong

            //}
        }

    }
}
