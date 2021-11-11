using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using Wikivox_Api.Models;

namespace Wikivox_Api.Services
{
    public class TrafficService
    {
        private readonly IMongoCollection<Traffic> _traffic;

        public TrafficService(IProjects_ApiDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _traffic = database.GetCollection<Traffic>(settings.TrafficCollectionName);
        }

        public async Task<List<Traffic>> GetAllAsync()
        {
            return await _traffic.Find(s => true).ToListAsync();
        }

        public async Task<Traffic> GetByIdAsync(string id)
        {
            return await _traffic.Find<Traffic>(s => s.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Traffic> CreateAsync(Traffic traffic)
        {
            await _traffic.InsertOneAsync(traffic);
            return traffic;
        }

        public async Task UpdateAsync(string id, Traffic traffic)
        {
            await _traffic.ReplaceOneAsync(s => s.Id == id, traffic);
        }

        public async Task DeleteAsync(string id)
        {
            await _traffic.DeleteOneAsync(s => s.Id == id);
        }
    }
}
