using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Wikivox_Api.Models
{
    [BsonIgnoreExtraElements]
    public class Traffic
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string country_code { get; set; }
        public string country_name { get; set; }
        public string city { get; set; }
        public string postal { get; set; }
        public decimal latitude { get; set; }
        public decimal longitude { get; set; }
        public string iPv4 { get; set; }
        public string state { get; set; }

        public DateTime timestamp { get; set; }
        public string url { get; set; }
    }
}
