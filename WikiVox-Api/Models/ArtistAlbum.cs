using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace Wikivox_Api.Models
{
    [BsonIgnoreExtraElements]
    public class ArtistAlbum
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string Artist { get; set; }
        [BsonIgnore]
        public Artist ArtistData { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> Albums { get; set; }
        [BsonIgnore]
        public List<Album> AlbumData { get; set; }
    }
}
