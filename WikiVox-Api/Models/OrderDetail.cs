using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wikivox_Api.Models
{
    [BsonIgnoreExtraElements]
    public class OrderDetail
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public Int32 OrderID { get; set; }
        public Int32 ProductID { get; set; }
        public double UnitPrice { get; set; }
        public Int32 Quantity { get; set; }
        public double Discount { get; set; }

        [BsonIgnore]
        public Product ProductData { get; set; }
        [BsonIgnore]
        public double ExtdPrice { get; set; }

    }
}
