using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using Wikivox_Api.Models;

namespace Wikivox_Api.Services
{
    public class ProjectsService
    {
        private readonly IMongoCollection<Projects> _projects;

        public ProjectsService(IProjects_ApiDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _projects = database.GetCollection<Projects>(settings.ProjectsCollectionName);
        }

        //public async Task<List<Projects>> GetAllAsync()
        //{
        //    return await _projects.Find(s => true).ToListAsync();
        //}

        public async Task<List<Projects>> GetAllAsync()
        {
            return await _projects
                .Find(s => s.Show == true)
                .SortBy(s => s.DisplayOrder)
                .ToListAsync();
        }

        public async Task<Projects> GetByIdAsync(string id)
        {
            return await _projects.Find<Projects>(s => s.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Projects> CreateAsync(Projects Projects)
        {
            await _projects.InsertOneAsync(Projects);
            return Projects;
        }

        public async Task UpdateAsync(string id, Projects Projects)
        {
            await _projects.ReplaceOneAsync(s => s.Id == id, Projects);
        }

        public async Task DeleteAsync(string id)
        {
            await _projects.DeleteOneAsync(s => s.Id == id);
        }
    }
}
