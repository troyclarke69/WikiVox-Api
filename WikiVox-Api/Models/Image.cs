using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wikivox_Api.Models
{
    [BsonIgnoreExtraElements]
    public class Image
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        //public Entity Entity { get; set; } //ex. 1 = Album, 2 = Musician, 3 = Album, 4 = Song, 5 = Genre
        [BsonRepresentation(BsonType.ObjectId)]
        public string Entity { get; set; }
        [BsonIgnore]
        public Entity EntityData { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string Resource { get; set; } //ex. Id of Album, etc

        // will act like a placeholder for whatever data we are looking at
        [BsonIgnore]
        public Object ResourceData { get; set; }

        public string Url { get; set; }
        public int PrimaryImage { get; set; }
    }
}
