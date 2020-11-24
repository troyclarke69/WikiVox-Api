using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wikivox_Api.Models
{
    [BsonIgnoreExtraElements]
    public class Blogg
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string markdown { get; set; }
        public string slug { get; set; }
        public string sanitizedHtml { get; set; }
        public DateTime createdAt { get; set; }

    }
}
