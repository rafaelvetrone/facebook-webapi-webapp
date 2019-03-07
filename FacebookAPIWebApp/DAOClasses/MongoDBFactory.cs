using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver;

namespace FacebookAPIWebApp.DAOClasses
{
    public class MongoDBFactory
    {
        static MongoClient client;
        static IMongoDatabase db;
        static MongoDBFactory()
        {
            client = new MongoClient("mongodb://localhost:27017");
        }

        public static IMongoDatabase GetMongoDatabase()
        {
            if (db == null)
                db = client.GetDatabase("facebookTestAPI");
            return db;
        }
    }
}