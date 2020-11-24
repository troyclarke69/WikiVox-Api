using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wikivox_Api.Models;

namespace Wikivox_Api.Services
{
    public class MusicianService
    {
        private readonly IMongoCollection<Musician> _musician;

        public MusicianService(IWikivox_ApiDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _musician = database.GetCollection<Musician>(settings.MusicianCollectionName);
        }

        public async Task<List<Musician>> GetAllAsync()
        {
            return await _musician.Find(s => true).ToListAsync();
        }

        public async Task<Musician> GetByIdAsync(string id)
        {
            return await _musician.Find<Musician>(s => s.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Musician> CreateAsync(Musician Musician)
        {
            await _musician.InsertOneAsync(Musician);
            return Musician;
        }

        public async Task UpdateAsync(string id, Musician Musician)
        {
            await _musician.ReplaceOneAsync(s => s.Id == id, Musician);
        }

        public async Task DeleteAsync(string id)
        {
            await _musician.DeleteOneAsync(s => s.Id == id);
        }
    }
}
