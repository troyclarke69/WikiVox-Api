using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wikivox_Api.Models
{
    [BsonIgnoreExtraElements]
    public class ArtistGenre
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string Artist { get; set; }
        [BsonIgnore]
        public Artist ArtistData { get; set; }

        public List<string> Genres { get; set; }
        [BsonIgnore]
        public List<Genre> GenreData { get; set; }
    }
}
