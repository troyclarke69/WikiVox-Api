using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wikivox_Api.Models;

namespace Wikivox_Api.Services
{
    public class ArtistMusicianService
    {
        private readonly IMongoCollection<ArtistMusician> _artistMusicians;

        public ArtistMusicianService(IWikivox_ApiDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _artistMusicians = database.GetCollection<ArtistMusician>(settings.ArtistMusicianCollectionName);
        }

        public async Task<List<ArtistMusician>> GetAllAsync()
        {
            return await _artistMusicians.Find(s => true).ToListAsync();
        }

        public async Task<ArtistMusician> GetByIdAsync(string id)
        {
            return await _artistMusicians.Find<ArtistMusician>(s => s.Id == id).FirstOrDefaultAsync();
        }

        public async Task<ArtistMusician> CreateAsync(ArtistMusician ArtistMusician)
        {
            await _artistMusicians.InsertOneAsync(ArtistMusician);
            return ArtistMusician;
        }

        public async Task UpdateAsync(string id, ArtistMusician ArtistMusician)
        {
            await _artistMusicians.ReplaceOneAsync(s => s.Id == id, ArtistMusician);
        }

        public async Task DeleteAsync(string id)
        {
            await _artistMusicians.DeleteOneAsync(s => s.Id == id);
        }
    }
}
