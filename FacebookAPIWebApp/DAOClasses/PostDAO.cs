using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver;
using MongoDB.Bson;

namespace FacebookAPIWebApp.DAOClasses
{
    public class PostDAO
    {
        public List<PostDO> Carregar()
        {
            IMongoDatabase db = MongoDBFactory.GetMongoDatabase();
            IMongoCollection<PostDO> accCollection = db.GetCollection<PostDO>("accounts");
            return accCollection.Find(new BsonDocument()).ToList<PostDO>();
        }

        public void Salvar(PostDO obj)
        {
            IMongoDatabase db = MongoDBFactory.GetMongoDatabase();
            IMongoCollection<PostDO> pCollection = db.GetCollection<PostDO>("posts");
            var result = pCollection.Find(x => x.Id == obj.Id).FirstOrDefault();
            if (result != null)
            {
                var filter = Builders<PostDO>.Filter.Eq("Id", obj.Id);
                var update = Builders<PostDO>.Update.Set("Message", obj.Message);
                pCollection.UpdateOne(filter, update);
            }
            else
                pCollection.InsertOne(obj);
        }

        public List<PostDO> CarregarPorData(DateTime data)
        {
            IMongoDatabase db = MongoDBFactory.GetMongoDatabase();
            IMongoCollection<PostDO> pCollection = db.GetCollection<PostDO>("posts");
            var result = pCollection.Find(x => x.CreatedTime >= data).ToList();

            result.OrderByDescending(x => x.CreatedTime).ToList();
            
            return result;
        }
    }
}