using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FacebookAPIWebApp.DAOClasses
{
    public class PostDO
    {
        [BsonElement("Id")]
        public string Id { get; set; }

        [BsonElement("message")]
        public string Message { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }

        [BsonElement("permalink_url")]
        public string PermalinkUrl { get; set; }

        [BsonElement("created_time")]
        public DateTime CreatedTime { get; set; }
    }
}