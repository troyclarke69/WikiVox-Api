namespace Wikivox_Api.Models
{
    public interface IProjects_ApiDatabaseSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string ProjectsCollectionName { get; set; }
        string ContactCollectionName { get; set; }
        string TrafficCollectionName { get; set; }


    }
    public class Projects_ApiDatabaseSettings : IProjects_ApiDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string ProjectsCollectionName { get; set; }
        public string ContactCollectionName { get; set; }
        public string TrafficCollectionName { get; set; }

    }
}
