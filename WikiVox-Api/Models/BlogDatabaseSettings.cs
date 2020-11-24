using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wikivox_Api.Models
{
   public interface IBlog_ApiDatabaseSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string ArticlesCollectionName { get; set; }

    }
    public class Blog_ApiDatabaseSettings : IBlog_ApiDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string ArticlesCollectionName { get; set; }
      
    }
}
