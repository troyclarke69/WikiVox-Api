using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using Wikivox_Api.Models;

namespace Wikivox_Api.Services
{
    public class CustomerService
    {
        private readonly IMongoCollection<Customer> _customer;

        public CustomerService(ICopd_ApiDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _customer = database.GetCollection<Customer>(settings.CustomerCollectionName);
        }

        //public async Task<List<Customer>> GetAllAsync()
        //{
        //    return await _customer.Find(s => true).ToListAsync();
        //}

        public async Task<PagedList<Customer>> GetAllAsync(CustomerParams customerParams)
        {
            //return await _customer.Find(s => true).ToListAsync();
            return PagedList<Customer>.ToPagedList(
                _customer.Find(s => true).ToList(),
                customerParams.PageNumber, customerParams.PageSize);

        }

        public async Task<List<Customer>> GetAllByCountryAsync(string country)
        {
            return await _customer.Find(s => s.Country == country).ToListAsync();
        }

        public async Task<Customer> GetByIdAsync(string id)
        {
            return await _customer.Find<Customer>(s => s.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Customer> GetByCustomerIdAsync(string customerId)
        {
            return await _customer.Find<Customer>(s => s.CustomerID == customerId).FirstOrDefaultAsync();
        }

        public async Task<Customer> GetByNameAsync(string name)
        {
            return await _customer.Find<Customer>(s => s.CompanyName == name).FirstOrDefaultAsync();
        }

        public async Task<Customer> CreateAsync(Customer Customer)
        {
            await _customer.InsertOneAsync(Customer);
            return Customer;
        }

        public async Task UpdateAsync(string id, Customer Customer)
        {
            await _customer.ReplaceOneAsync(s => s.Id == id, Customer);
        }

        public async Task DeleteAsync(string id)
        {
            await _customer.DeleteOneAsync(s => s.Id == id);
        }
    }
}
