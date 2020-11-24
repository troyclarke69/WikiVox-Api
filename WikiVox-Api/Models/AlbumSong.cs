using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wikivox_Api.Models
{
    [BsonIgnoreExtraElements]
    public class AlbumSong
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string Album { get; set; }
        [BsonIgnore]
        public Album AlbumData { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> Songs { get; set; }
        [BsonIgnore]
        public List<Song> SongData { get; set; }

        //[BsonRepresentation(BsonType.ObjectId)]
        //public string Artist { get; set; }
        //[BsonIgnore]
        //public Artist ArtistData { get; set; }

        //public int TrackNumber { get; set; }
    }
}
