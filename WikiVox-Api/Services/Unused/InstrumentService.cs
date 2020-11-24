using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wikivox_Api.Models;

namespace Wikivox_Api.Services
{
    public class InstrumentService
    {
        private readonly IMongoCollection<Instrument> _instrument;

        public InstrumentService(IWikivox_ApiDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _instrument = database.GetCollection<Instrument>(settings.InstrumentCollectionName);
        }

        public async Task<List<Instrument>> GetAllAsync()
        {
            return await _instrument.Find(s => true).ToListAsync();
        }

        public async Task<Instrument> GetByIdAsync(string id)
        {
            return await _instrument.Find<Instrument>(s => s.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Instrument> CreateAsync(Instrument Instrument)
        {
            await _instrument.InsertOneAsync(Instrument);
            return Instrument;
        }

        public async Task UpdateAsync(string id, Instrument Instrument)
        {
            await _instrument.ReplaceOneAsync(s => s.Id == id, Instrument);
        }

        public async Task DeleteAsync(string id)
        {
            await _instrument.DeleteOneAsync(s => s.Id == id);
        }
    }
}
