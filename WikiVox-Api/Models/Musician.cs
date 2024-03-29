﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Wikivox_Api.Models
{
    [BsonIgnoreExtraElements]
    public class Musician
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string MusicianName { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Bio { get; set; }
        public DateTime Birth { get; set; }
        public DateTime Death { get; set; }
        public Boolean isActive { get; set; }
        public string HomeTown { get; set; }
        public string HomeCountry { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> Images { get; set; }

        [BsonIgnore]
        public List<Image> ImageData { get; set; }
    }
}
