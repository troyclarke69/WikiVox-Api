using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Wikivox_Api.Models
{
    [BsonIgnoreExtraElements]
    public class Order
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public Int32 OrderID { get; set; }
        public string CustomerID { get; set; }
        public Int32 EmployeeID { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime RequiredDate { get; set; }
        public DateTime ShippedDate { get; set; }
        public Int32 ShipVia { get; set; }
        public double Freight { get; set; }
        public string ShipName { get; set; }
        public string ShipAddress { get; set; }
        public string ShipCity { get; set; }
        public string ShipRegion { get; set; }
        public string ShipPostalCode { get; set; }
        public string ShipCountry { get; set; }

        [BsonIgnore]
        public Customer CustomerData { get; set; }
        [BsonIgnore]
        public List<OrderDetail> DetailData { get; set; }

        [BsonIgnore]
        public double SubTotal { get; set; }
        [BsonIgnore]
        public double OrderTotal { get; set; }

        //[BsonIgnore]
        //public List<Product> ProductData { get; set; }

    }
}
