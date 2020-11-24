using System;
using System.Collections.Generic;
using Wikivox_Api.Models;

namespace Wikivox_Api.Dtos
{

    // API will expose the following fields for the Order class
    public class OrderReadDto
    {
        public string Id { get; set; }
        public Int32 OrderID { get; set; }
        public string CustomerID { get; set; }
        //public Int32 EmployeeID { get; set; }
        public DateTime OrderDate { get; set; }
        //public DateTime RequiredDate { get; set; }
        public DateTime ShippedDate { get; set; }
        //public Int32 ShipVia { get; set; }
        public double Freight { get; set; }
        //public string ShipName { get; set; }
        //public string ShipAddress { get; set; }
        public string ShipCity { get; set; }
        //public string ShipRegion { get; set; }
        //public string ShipPostalCode { get; set; }
        public string ShipCountry { get; set; }
        public double SubTotal { get; set; }
        public double OrderTotal { get; set; }
        public Customer CustomerData { get; set; }
        public List<OrderDetail> DetailData { get; set; }
    }
}
