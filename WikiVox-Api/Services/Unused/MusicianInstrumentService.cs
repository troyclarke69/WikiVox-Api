using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wikivox_Api.Models;

namespace Wikivox_Api.Services
{
    public class MusicianInstrumentService
    {
        private readonly IMongoCollection<MusicianInstrument> _musicianInstrument;

        public MusicianInstrumentService(IWikivox_ApiDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _musicianInstrument = database.GetCollection<MusicianInstrument>(settings.MusicianInstrumentCollectionName);
        }

        public async Task<List<MusicianInstrument>> GetAllAsync()
        {
            return await _musicianInstrument.Find(s => true).ToListAsync();
        }

        public async Task<MusicianInstrument> GetByIdAsync(string id)
        {
            return await _musicianInstrument.Find<MusicianInstrument>(s => s.Id == id).FirstOrDefaultAsync();
        }

        public async Task<MusicianInstrument> CreateAsync(MusicianInstrument MusicianInstrument)
        {
            await _musicianInstrument.InsertOneAsync(MusicianInstrument);
            return MusicianInstrument;
        }

        public async Task UpdateAsync(string id, MusicianInstrument MusicianInstrument)
        {
            await _musicianInstrument.ReplaceOneAsync(s => s.Id == id, MusicianInstrument);
        }

        public async Task DeleteAsync(string id)
        {
            await _musicianInstrument.DeleteOneAsync(s => s.Id == id);
        }
    }
}
