using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wikivox_Api.Models;

namespace Wikivox_Api.Services
{
    public class SongService
    {
        private readonly IMongoCollection<Song> _song;

        public SongService(IWikivox_ApiDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _song = database.GetCollection<Song>(settings.SongCollectionName);
        }

        public async Task<List<Song>> GetAllAsync()
        {
            return await _song.Find(s => true).ToListAsync();
        }

        public async Task<Song> GetByIdAsync(string id)
        {
            return await _song.Find<Song>(s => s.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Song> CreateAsync(Song Song)
        {
            await _song.InsertOneAsync(Song);
            return Song;
        }

        public async Task UpdateAsync(string id, Song Song)
        {
            await _song.ReplaceOneAsync(s => s.Id == id, Song);
        }

        public async Task DeleteAsync(string id)
        {
            await _song.DeleteOneAsync(s => s.Id == id);
        }
    }
}
