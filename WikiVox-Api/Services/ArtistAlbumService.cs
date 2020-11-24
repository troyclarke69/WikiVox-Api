using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using Wikivox_Api.Models;

namespace Wikivox_Api.Services
{
    public class ArtistAlbumService
    {
        private readonly IMongoCollection<ArtistAlbum> _artistAlbum;

        public ArtistAlbumService(IWikivox_ApiDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _artistAlbum = database.GetCollection<ArtistAlbum>(settings.ArtistAlbumCollectionName);
        }

        public async Task<List<ArtistAlbum>> GetAllAsync()
        {
            return await _artistAlbum.Find(s => true).ToListAsync();
        }

        public async Task<ArtistAlbum> GetByIdAsync(string id)
        {
            return await _artistAlbum.Find<ArtistAlbum>(s => s.Id == id).FirstOrDefaultAsync();
        }

        public async Task<ArtistAlbum> CreateAsync(ArtistAlbum ArtistAlbum)
        {
            await _artistAlbum.InsertOneAsync(ArtistAlbum);
            return ArtistAlbum;
        }

        public async Task UpdateAsync(string id, ArtistAlbum ArtistAlbum)
        {
            await _artistAlbum.ReplaceOneAsync(s => s.Id == id, ArtistAlbum);
        }

        public async Task DeleteAsync(string id)
        {
            await _artistAlbum.DeleteOneAsync(s => s.Id == id);
        }
    }
}
