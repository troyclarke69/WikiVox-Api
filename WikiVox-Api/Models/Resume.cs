using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wikivox_Api.Models
{
    [BsonIgnoreExtraElements]
    public class About
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string occupation { get; set; }
        public string image { get; set; }
        public string bio { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
    }

    public class Work
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
       
    }
}
