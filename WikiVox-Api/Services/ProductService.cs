using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wikivox_Api.Models;

namespace Wikivox_Api.Services
{
    public class ProductService
    {
        private readonly IMongoCollection<Product> _product;

        public ProductService(ICopd_ApiDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _product = database.GetCollection<Product>(settings.ProductCollectionName);
        }

        //public async Task<List<Product>> GetAllAsync()
        //{
        //    return await _product.Find(s => true).ToListAsync();
        //}

        public async Task<PagedList<Product>> GetAllAsync(ProductParams productParams)
        {
            //return await _customer.Find(s => true).ToListAsync();
            return PagedList<Product>.ToPagedList(
                _product.Find(s => true).ToList(),
                productParams.PageNumber, productParams.PageSize);

        }

        public async Task<Product> GetByIdAsync(string id)
        {
            return await _product.Find<Product>(s => s.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Product> GetByProductIdAsync(int productId)
        {
            return await _product.Find<Product>(s => s.ProductID == productId).FirstOrDefaultAsync();
        }

        public async Task<Product> CreateAsync(Product Product)
        {
            await _product.InsertOneAsync(Product);
            return Product;
        }

        public async Task UpdateAsync(string id, Product Product)
        {
            await _product.ReplaceOneAsync(s => s.Id == id, Product);
        }

        public async Task DeleteAsync(string id)
        {
            await _product.DeleteOneAsync(s => s.Id == id);
        }
    }
}
