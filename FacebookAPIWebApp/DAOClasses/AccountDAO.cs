using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver;
using MongoDB.Bson;

namespace FacebookAPIWebApp.DAOClasses
{
    public class AccountDAO
    {
        public List<AccountDO> Carregar()
        {
            IMongoDatabase db = MongoDBFactory.GetMongoDatabase();
            IMongoCollection<AccountDO> accCollection = db.GetCollection<AccountDO>("accounts");
            return accCollection.Find(new BsonDocument()).ToList<AccountDO>();
        }

        public void Salvar(AccountDO obj)
        {
            IMongoDatabase db = MongoDBFactory.GetMongoDatabase();
            IMongoCollection<AccountDO> accCollection = db.GetCollection<AccountDO>("accounts");
            var result = accCollection.Find(x => x.Id == obj.Id).FirstOrDefault();
            if (result != null)
            {
                var filter = Builders<AccountDO>.Filter.Eq("Id", obj.Id);
                var update = Builders<AccountDO>.Update.Set("FirstName", obj.FirstName);
                accCollection.UpdateOne(filter, update);
            }
            else
                accCollection.InsertOne(obj);
        }
    }
}