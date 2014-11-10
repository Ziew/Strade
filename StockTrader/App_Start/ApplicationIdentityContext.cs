using System;
using AspNet.Identity.MongoDB;
using MongoDB.Driver;

namespace StockTrader
{
    public class ApplicationIdentityContext : IdentityContext, IDisposable
    {
        private ApplicationIdentityContext(MongoCollection users, MongoCollection roles)
            : base(users, roles)
        {
        }

        public void Dispose()
        {
        }

        public static ApplicationIdentityContext Create()
        {
            // todo add settings where appropriate to switch server & database in your own application
            var client = new MongoClient("mongodb://localhost:27017");
            MongoDatabase database = client.GetServer().GetDatabase("mydb");
            MongoCollection<IdentityUser> users = database.GetCollection<IdentityUser>("users");
            MongoCollection<IdentityRole> roles = database.GetCollection<IdentityRole>("roles");
            return new ApplicationIdentityContext(users, roles);
        }
    }
}