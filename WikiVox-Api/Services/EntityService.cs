using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wikivox_Api.Models;

namespace Wikivox_Api.Services
{
    public class EntityService
    {
        private readonly IMongoCollection<Entity> _entity;

        public EntityService(IWikivox_ApiDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _entity = database.GetCollection<Entity>(settings.EntityCollectionName);
        }

        public async Task<List<Entity>> GetAllAsync()
        {
            return await _entity.Find(s => true).ToListAsync();
        }

        public async Task<Entity> GetByIdAsync(string id)
        {
            return await _entity.Find<Entity>(s => s.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Entity> CreateAsync(Entity Entity)
        {
            await _entity.InsertOneAsync(Entity);
            return Entity;
        }

        public async Task UpdateAsync(string id, Entity Entity)
        {
            await _entity.ReplaceOneAsync(s => s.Id == id, Entity);
        }

        public async Task DeleteAsync(string id)
        {
            await _entity.DeleteOneAsync(s => s.Id == id);
        }
    }
}
