using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wikivox_Api.Models;

namespace Wikivox_Api.Services
{
    public class ArtistGenreService
    {
        private readonly IMongoCollection<ArtistGenre> _artistGenre;

        public ArtistGenreService(IWikivox_ApiDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _artistGenre = database.GetCollection<ArtistGenre>(settings.ArtistGenreCollectionName);
        }

        public async Task<List<ArtistGenre>> GetAllAsync()
        {
            return await _artistGenre.Find(s => true).ToListAsync();
        }

        public async Task<ArtistGenre> GetByIdAsync(string id)
        {
            return await _artistGenre.Find<ArtistGenre>(s => s.Id == id).FirstOrDefaultAsync();
        }

        public async Task<ArtistGenre> CreateAsync(ArtistGenre ArtistGenre)
        {
            await _artistGenre.InsertOneAsync(ArtistGenre);
            return ArtistGenre;
        }

        public async Task UpdateAsync(string id, ArtistGenre ArtistGenre)
        {
            await _artistGenre.ReplaceOneAsync(s => s.Id == id, ArtistGenre);
        }

        public async Task DeleteAsync(string id)
        {
            await _artistGenre.DeleteOneAsync(s => s.Id == id);
        }
    }
}
