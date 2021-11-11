using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wikivox_Api.Models;

namespace Wikivox_Api.Services
{
    public class ArtistService
    {
        private readonly IMongoCollection<Artist> _artist;

        public ArtistService(IWikivox_ApiDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _artist = database.GetCollection<Artist>(settings.ArtistCollectionName);
        }

        public async Task<List<Artist>> GetAllAsync()
        {
            // default load based on PRIMARY field
            // control the amount of records onload
            return await _artist.Find(s => s.Primary == true)
                .SortBy(s => s.ArtistName)
                .ToListAsync();
        }

        public async Task<List<Artist>> GetFeaturedAsync()
        {
            return await _artist.Find(s => s.Featured == true)
                .SortBy(s => s.ArtistName)
                .ToListAsync();
        }

        public async Task<List<Artist>> GetAllByArtistNameAsync(string artistName)
        {
            return await _artist.Find(s => s.ArtistName.ToLower().Contains(artistName.ToLower()))
                .SortBy(s => s.ArtistName)
                .ToListAsync();
        }

        public async Task<Artist> GetByIdAsync(string id)
        {
            return await _artist.Find<Artist>(s => s.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Artist> CreateAsync(Artist Artist)
        {
            await _artist.InsertOneAsync(Artist);
            return Artist;
        }

        public async Task UpdateAsync(string id, Artist Artist)
        {
            await _artist.ReplaceOneAsync(s => s.Id == id, Artist);
        }

        public async Task DeleteAsync(string id)
        {
            await _artist.DeleteOneAsync(s => s.Id == id);
        }
    }
}
