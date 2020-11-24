using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using Wikivox_Api.Models;

namespace Wikivox_Api.Services
{
    public class BlogService
    {
        private readonly IMongoCollection<Blogg> _blog;

        public BlogService(IBlog_ApiDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _blog = database.GetCollection<Blogg>(settings.ArticlesCollectionName);
        }

        //public async Task<List<Blogg>> GetAllAsync()
        //{
        //    return await _blog.Find(s => true).ToListAsync();
        //}

        public async Task<List<Blogg>> GetAllAsync()
        {
            return await _blog.Find(s => true)
                .SortByDescending(x => x.createdAt)
                .ToListAsync();
        }

        public async Task<Blogg> GetByIdAsync(string id)
        {
            return await _blog.Find<Blogg>(s => s.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Blogg> CreateAsync(Blogg Blogg)
        {
            await _blog.InsertOneAsync(Blogg);
            return Blogg;
        }

        public async Task UpdateAsync(string id, Blogg Blogg)
        {
            await _blog.ReplaceOneAsync(s => s.Id == id, Blogg);
        }

        public async Task DeleteAsync(string id)
        {
            await _blog.DeleteOneAsync(s => s.Id == id);
        }
    }
}
