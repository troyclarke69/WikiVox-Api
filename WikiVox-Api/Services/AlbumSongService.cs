using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wikivox_Api.Models;

namespace Wikivox_Api.Services
{
    public class AlbumSongService
    {
        private readonly IMongoCollection<AlbumSong> _albumSong;

        public AlbumSongService(IWikivox_ApiDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _albumSong = database.GetCollection<AlbumSong>(settings.AlbumSongCollectionName);
        }

        public async Task<List<AlbumSong>> GetAllAsync()
        {
            return await _albumSong.Find(s => true).ToListAsync();
        }

        public async Task<AlbumSong> GetByIdAsync(string id)
        {
            return await _albumSong.Find<AlbumSong>(s => s.Id == id).FirstOrDefaultAsync();
        }

        public async Task<AlbumSong> CreateAsync(AlbumSong AlbumSong)
        {
            await _albumSong.InsertOneAsync(AlbumSong);
            return AlbumSong;
        }

        public async Task UpdateAsync(string id, AlbumSong AlbumSong)
        {
            await _albumSong.ReplaceOneAsync(s => s.Id == id, AlbumSong);
        }

        public async Task DeleteAsync(string id)
        {
            await _albumSong.DeleteOneAsync(s => s.Id == id);
        }
    }
}
