using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wikivox_Api.Models;

namespace Wikivox_Api.Services
{
    public class GenreService
    {
        private readonly IMongoCollection<Genre> _genre;

        public GenreService(IWikivox_ApiDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _genre = database.GetCollection<Genre>(settings.GenreCollectionName);
        }

        public async Task<List<Genre>> GetAllAsync()
        {
            return await _genre.Find(s => true).ToListAsync();
        }

        public async Task<Genre> GetByIdAsync(string id)
        {
            return await _genre.Find<Genre>(s => s.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Genre> CreateAsync(Genre Genre)
        {
            await _genre.InsertOneAsync(Genre);
            return Genre;
        }

        public async Task UpdateAsync(string id, Genre Genre)
        {
            await _genre.ReplaceOneAsync(s => s.Id == id, Genre);
        }

        public async Task DeleteAsync(string id)
        {
            await _genre.DeleteOneAsync(s => s.Id == id);
        }
    }
}
