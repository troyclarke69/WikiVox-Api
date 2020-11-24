using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Wikivox_Api.Models;

namespace Wikivox_Api.Services
{
    public class OrderService
    {
        private readonly IMongoCollection<Order> _order;
        private readonly OrderDetailService _orderDetailService;

        public OrderService(ICopd_ApiDatabaseSettings settings, OrderDetailService orderDetailService)
        {
            _orderDetailService = orderDetailService;

            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _order = database.GetCollection<Order>(settings.OrderCollectionName);
        }

        public async Task<double> GetOrderTotal(Int32 orderId)
        {
            // flag: getProduct - set false when product details are not needed
            Boolean getProduct = false;

            // get all order details...
            List<OrderDetail> details = await _orderDetailService.GetAllByOrderIdAsync(orderId, getProduct);
            double orderTotal = 0;
            double unitPrice = 0;
            Int32 quantity = 0;
            double discount = 0;
            double lineTotal = 0;

            foreach (var _detail in details)
            {
                unitPrice = _detail.UnitPrice;
                quantity = _detail.Quantity;
                discount = _detail.Discount;
                lineTotal = (unitPrice * quantity) - ((unitPrice * quantity) * discount);
                orderTotal += lineTotal;
            }

            return orderTotal;
        }

        //public async Task<PagedList<Order>> GetAllAsync(OrderParams orderParams)
        //{
        //    // Note: There is an async issue here, but the code runs synchronously ...

        //    return PagedList<Order>.ToPagedList(_order.Find(s => true)
        //            .SortByDescending(s => s.OrderDate)
        //            .ToList(),
        //            orderParams.PageNumber, orderParams.PageSize);

            //List<Order> orderList = new List<Order>();

            //foreach (var _order in orders)
            //{
            //    _order.SubTotal = await GetOrderTotal(_order.OrderID);
            //    _order.OrderTotal = _order.SubTotal + _order.Freight;
            //    orderList.Add(_order);
            //}

            //return orderList;
        //}

        public async Task<PagedList<Order>> GetAllAsync(OrderParams orderParams)
        {
            // Note: There is an async issue here, but the code runs synchronously ...

            // *****************************************************************************************
            // orig: returns dataset with page info ... does not include totals
            //return PagedList<Order>.ToPagedList(_order.Find(s => true)
            //        .SortByDescending(s => s.OrderDate)
            //        .ToList(),
            //        orderParams.PageNumber, orderParams.PageSize);
            // *****************************************************************************************

            var orders = PagedList<Order>.ToPagedList(
                _order.Find(s => true).SortByDescending(s => s.OrderDate).ToList(), 
                orderParams.PageNumber, orderParams.PageSize);

            // alternative method: create another empty object and fill with details from 'orders' ... + the sub & order total
            //var totalCount = orders.TotalCount;
            //PagedList<Order> orderList = new PagedList<Order>(
            //    totalCount, orderParams.PageNumber, orderParams.PageSize);

            foreach (var order in orders)
            {
                order.SubTotal = await GetOrderTotal(order.OrderID);
                order.OrderTotal = order.SubTotal + order.Freight;

                //alt: fill the new object
                //orderList.Add(order);
            }

            // alt: return orderList;
            return orders;
        }

        public async Task<List<Order>> GetAllAsync1(OrderParams orderParams)
        {
            //return await _order.Find(s => true).ToListAsync();

            //IEnumerable<Order> orders = await _order
            //    .Find(s => true)
            //    .Limit(100)
            //    .SortByDescending(s => s.OrderDate)
            //    .ToListAsync();

            // rev. pagination:
            IEnumerable<Order> orders = await _order
                .Find(s => true)
                .Skip((orderParams.PageNumber - 1) * orderParams.PageSize)
                .Limit(orderParams.PageSize)
                .SortByDescending(s => s.OrderDate)
                .ToListAsync();

            List<Order> orderList = new List<Order>();

            foreach (var _order in orders)
            {
                _order.SubTotal = await GetOrderTotal(_order.OrderID);
                _order.OrderTotal = _order.SubTotal + _order.Freight;
                orderList.Add(_order);
            }

            return orderList;
        }

        public async Task<Order> GetByIdAsync(string id)
        {
            return await _order.Find<Order>(s => s.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Order>> GetByCustomerIdAsync(string customerId)
        {
            return await _order.Find(s => s.CustomerID == customerId).ToListAsync();
        }

        public async Task<Order> GetByOrderIdAsync(Int32 orderId)
        {
            return await _order.Find<Order>(s => s.OrderID == orderId).FirstOrDefaultAsync();
        }

        public async Task<Order> CreateAsync(Order Order)
        {
            await _order.InsertOneAsync(Order);
            return Order;
        }

        public async Task UpdateAsync(string id, Order Order)
        {
            await _order.ReplaceOneAsync(s => s.Id == id, Order);
        }

        public async Task DeleteAsync(string id)
        {
            await _order.DeleteOneAsync(s => s.Id == id);
        }
    }
}
