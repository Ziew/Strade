using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            return this.MongoConnectionHandler.MongoCollection.FindOne(entityQuery);
        }

        public void AddStock(string userId, Stocks stock)
        {
            

            var updateResult = this.MongoConnectionHandler.MongoCollection.Update(
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

        public void AddTransaction(string userId,string companySymbol, int stockNumber, TransactionHistory transactionHistory)
        {


            var updateResult = this.MongoConnectionHandler.MongoCollection.Update(
                    Query<StockWallet>.EQ(p => p.UserEmail, userId),
                    Update<StockWallet>.Push(p => p.OwnedStocks.FirstOrDefault(n => n.CompanySymbol == companySymbol).TransactionHistories, transactionHistory),
                    new MongoUpdateOptions
                    {
                        WriteConcern = WriteConcern.Acknowledged
                    });
            var updateResult2 = this.MongoConnectionHandler.MongoCollection.Update(
                    Query<StockWallet>.EQ(p => p.UserEmail, userId),
                    Update<StockWallet>.Inc(p => p.OwnedStocks.FirstOrDefault(n => n.CompanySymbol == companySymbol).NumberOfStocks, stockNumber),
                    new MongoUpdateOptions
                    {
                        WriteConcern = WriteConcern.Acknowledged
                    });

            if (updateResult.DocumentsAffected == 0)
            {
                //// Something went wrong

            }
        }

    }
}
