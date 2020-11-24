using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wikivox_Api.Models;

namespace Wikivox_Api.Services
{
    public class ImageService
    {
        private readonly IMongoCollection<Image> _image;

        public ImageService(IWikivox_ApiDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _image = database.GetCollection<Image>(settings.ImageCollectionName);
        }

        public async Task<List<Image>> GetAllAsync()
        {
            return await _image.Find(s => true).ToListAsync();
        }

        public async Task<Image> GetByIdAsync(string id)
        {
            return await _image.Find<Image>(s => s.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Image> CreateAsync(Image Image)
        {
            await _image.InsertOneAsync(Image);
            return Image;
        }

        public async Task UpdateAsync(string id, Image Image)
        {
            await _image.ReplaceOneAsync(s => s.Id == id, Image);
        }

        public async Task DeleteAsync(string id)
        {
            await _image.DeleteOneAsync(s => s.Id == id);
        }
    }
}
