using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace Wikivox_Api.Models
{
    [BsonIgnoreExtraElements]
    public class Album
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public int YrReleased { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> Images { get; set; }

        [BsonIgnore]
        public List<Image> ImageData { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> Songs { get; set; }

        [BsonIgnore]
        public List<Song> SongData { get; set; }
    }
}
