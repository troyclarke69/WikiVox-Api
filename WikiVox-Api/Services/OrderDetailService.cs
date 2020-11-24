using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wikivox_Api.Models;

namespace Wikivox_Api.Services
{
    public class OrderDetailService
    {
        private readonly IMongoCollection<OrderDetail> _orderDetail;

        private readonly ProductService _productService;

        public OrderDetailService(ICopd_ApiDatabaseSettings settings, ProductService productService)
        {
            _productService = productService;

            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _orderDetail = database.GetCollection<OrderDetail>(settings.OrderDetailCollectionName);
        }

        public async Task<List<OrderDetail>> GetAllAsync()
        {
            return await _orderDetail.Find(s => true).ToListAsync();
        }

        public async Task<List<OrderDetail>> GetAllByOrderIdAsync(Int32 orderId, Boolean getProduct)
        {
            // orig. without productData:
            //return await _orderDetail.Find(s => s.OrderID == orderId).ToListAsync();

            // calc extd price:
            double extdPrice = 0;
            double unitPrice = 0;
            Int32 quantity = 0;
            double discount = 0;

            List<OrderDetail> detailList = new List<OrderDetail>();
            var OrderDetail = await _orderDetail.Find(s => s.OrderID == orderId).ToListAsync();
            if (OrderDetail != null)
            {

                foreach (var _detail in OrderDetail)
                {
                    unitPrice = _detail.UnitPrice;
                    quantity = _detail.Quantity;
                    discount = _detail.Discount;
                    extdPrice = (unitPrice * quantity) - ((unitPrice * quantity) * discount);
                    _detail.ExtdPrice = extdPrice;

                    if (getProduct)
                    {
                        if (_detail.ProductID != 0)
                        {
                            // call orderDetails method to get all detail objects
                            var product = await _productService.GetByProductIdAsync(_detail.ProductID);
                            _detail.ProductData = product;

                        }
                    }
                    detailList.Add(_detail);
                }
            }
            return detailList;
        }

        public async Task<OrderDetail> GetByIdAsync(string id)
        {
            return await _orderDetail.Find<OrderDetail>(s => s.Id == id).FirstOrDefaultAsync();
        }

        public async Task<OrderDetail> CreateAsync(OrderDetail OrderDetail)
        {
            await _orderDetail.InsertOneAsync(OrderDetail);
            return OrderDetail;
        }

        public async Task UpdateAsync(string id, OrderDetail OrderDetail)
        {
            await _orderDetail.ReplaceOneAsync(s => s.Id == id, OrderDetail);
        }

        public async Task DeleteAsync(string id)
        {
            await _orderDetail.DeleteOneAsync(s => s.Id == id);
        }
    }
}
