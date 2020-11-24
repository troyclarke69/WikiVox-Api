using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using Wikivox_Api.Models;

namespace Wikivox_Api.Services
{
    public class ContactService
    {
        private readonly IMongoCollection<Contact> _contact;

        public ContactService(IProjects_ApiDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _contact = database.GetCollection<Contact>(settings.ContactCollectionName);
        }

        public async Task<List<Contact>> GetAllAsync()
        {
            return await _contact.Find(s => true).ToListAsync();
        }

        public async Task<Contact> GetByIdAsync(string id)
        {
            return await _contact.Find<Contact>(s => s.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Contact> CreateAsync(Contact Contact)
        {
            await _contact.InsertOneAsync(Contact);
            return Contact;
        }

        public async Task UpdateAsync(string id, Contact Contact)
        {
            await _contact.ReplaceOneAsync(s => s.Id == id, Contact);
        }

        public async Task DeleteAsync(string id)
        {
            await _contact.DeleteOneAsync(s => s.Id == id);
        }
    }
}
