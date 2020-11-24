using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wikivox_Api.Models;

namespace Wikivox_Api.Services
{
    public class ArtistSongService
    {
        private readonly IMongoCollection<ArtistSong> _artistSongs;

        public ArtistSongService(IWikivox_ApiDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _artistSongs = database.GetCollection<ArtistSong>(settings.ArtistSongCollectionName);
        }

        public async Task<List<ArtistSong>> GetAllAsync()
        {
            return await _artistSongs.Find(s => true).ToListAsync();
        }

        public async Task<ArtistSong> GetByIdAsync(string id)
        {
            return await _artistSongs.Find<ArtistSong>(s => s.Id == id).FirstOrDefaultAsync();
        }

        public async Task<ArtistSong> CreateAsync(ArtistSong ArtistSong)
        {
            await _artistSongs.InsertOneAsync(ArtistSong);
            return ArtistSong;
        }

        public async Task UpdateAsync(string id, ArtistSong ArtistSong)
        {
            await _artistSongs.ReplaceOneAsync(s => s.Id == id, ArtistSong);
        }

        public async Task DeleteAsync(string id)
        {
            await _artistSongs.DeleteOneAsync(s => s.Id == id);
        }
    }
}
