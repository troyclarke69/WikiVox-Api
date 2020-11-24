using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wikivox_Api.Models
{
    public interface ICopd_ApiDatabaseSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string CustomerCollectionName { get; set; }
        string ProductCollectionName { get; set; }
        string OrderCollectionName { get; set; }
        string OrderDetailCollectionName { get; set; }

    }
    public class Copd_ApiDatabaseSettings : ICopd_ApiDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string CustomerCollectionName { get; set; }
        public string ProductCollectionName { get; set; }
        public string OrderCollectionName { get; set; }
        public string OrderDetailCollectionName { get; set; }
    }
}
