using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Wikivox_Api.Models
{
    [BsonIgnoreExtraElements]
    public class Artist
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Name")]
        public string ArtistName { get; set; }
        public string Slug { get; set; }

        public string Bio { get; set; }
        [Display(Name = "Year Formed")]
        public Int32 YrFormed { get; set; }
        [Display(Name = "Year Ended")]
        public Int32 YrEnded { get; set; }
        [Display(Name = "Active")]
        public int isActive { get; set; }
        [Display(Name = "Home Town")]
        public string HomeTown { get; set; }
        [Display(Name = "Home Country")]
        public string HomeCountry { get; set; }
        public Boolean Featured { get; set; }
        public Boolean Primary { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> Images { get; set; }
        [BsonIgnore]
        public List<Image> ImageData { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> Musicians { get; set; }
        [BsonIgnore]
        public List<Musician> MusicianData { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> Albums { get; set; }
        [BsonIgnore]
        public List<Album> AlbumData { get; set; }


    }
}
