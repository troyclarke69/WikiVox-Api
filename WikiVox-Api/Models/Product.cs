using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Wikivox_Api.Models
{
    [BsonIgnoreExtraElements]
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public Int32 ProductID { get; set; }
        public string ProductName { get; set; }
        public Int32 SupplierID { get; set; }
        public Int32 CategoryID { get; set; }
        public string QuantityPerUnit { get; set; }
        public double UnitPrice { get; set; }
        public Int32 UnitsInStock { get; set; }
        public Int32 UnitsOnOrder { get; set; }
        public Int32 ReorderLevel { get; set; }
        public Boolean Discontinued { get; set; }

    }
}
