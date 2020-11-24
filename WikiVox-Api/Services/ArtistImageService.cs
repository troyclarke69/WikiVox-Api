using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wikivox_Api.Models;

namespace Wikivox_Api.Services
{
    public class ArtistImageService
    {
        private readonly IMongoCollection<ArtistImage> _artistImage;

        public ArtistImageService(IWikivox_ApiDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _artistImage = database.GetCollection<ArtistImage>(settings.ArtistImageCollectionName);
        }

        public async Task<List<ArtistImage>> GetAllAsync()
        {
            return await _artistImage.Find(s => true).ToListAsync();
        }

        public async Task<ArtistImage> GetByIdAsync(string id)
        {
            return await _artistImage.Find<ArtistImage>(s => s.Id == id).FirstOrDefaultAsync();
        }

        public async Task<ArtistImage> CreateAsync(ArtistImage ArtistImage)
        {
            await _artistImage.InsertOneAsync(ArtistImage);
            return ArtistImage;
        }

        public async Task UpdateAsync(string id, ArtistImage ArtistImage)
        {
            await _artistImage.ReplaceOneAsync(s => s.Id == id, ArtistImage);
        }

        public async Task DeleteAsync(string id)
        {
            await _artistImage.DeleteOneAsync(s => s.Id == id);
        }
    }
}
