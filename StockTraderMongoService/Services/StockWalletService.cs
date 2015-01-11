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
    public class StockWalletService : EntityService<StockWallet>
    {
        public override void Update(StockWallet entity)
        {
            throw new NotImplementedException();
        }

        public virtual StockWallet GetByUser(string userEmail)
        {
            var entityQuery = Query<StockWallet>.EQ(e => e.UserEmail, userEmail);
            return MongoConnectionHandler.MongoCollection.FindOne(entityQuery);
        }

        public void AddStock(string userId, Stocks stock)
        {


            var updateResult = MongoConnectionHandler.MongoCollection.Update(
                    Query<StockWallet>.EQ(p => p.UserEmail, userId),
                    Update<StockWallet>.Push(p => p.OwnedStocks, stock),
                    new MongoUpdateOptions
                    {
                        WriteConcern = WriteConcern.Acknowledged
                    });

            if (updateResult.DocumentsAffected == 0)
            {
                //// Something went wrong

            }
        }

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
