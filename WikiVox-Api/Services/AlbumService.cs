using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wikivox_Api.Models;
using MongoDB.Driver;

namespace Wikivox_Api.Services
{
    public class AlbumService
    {
        private readonly IMongoCollection<Album> _albums;

        public AlbumService(IWikivox_ApiDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _albums = database.GetCollection<Album>(settings.AlbumCollectionName);
        }

        public async Task<List<Album>> GetAllAsync()
        {
            return await _albums.Find(s => true).ToListAsync();
        }

        public async Task<Album> GetByIdAsync(string id)
        {
            return await _albums.Find<Album>(s => s.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Album> CreateAsync(Album Album)
        {
            await _albums.InsertOneAsync(Album);
            return Album;
        }

        public async Task UpdateAsync(string id, Album Album)
        {
            await _albums.ReplaceOneAsync(s => s.Id == id, Album);
        }

        public async Task DeleteAsync(string id)
        {
            await _albums.DeleteOneAsync(s => s.Id == id);
        }
    }
}
