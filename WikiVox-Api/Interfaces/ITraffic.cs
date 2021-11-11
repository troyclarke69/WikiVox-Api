using System.Collections.Generic;
using Wikivox_Api.Models;

namespace Wikivox_Api.Interfaces
{
    public interface ITraffic
    {
        IEnumerable<Traffic> GetAll();
        IEnumerable<Traffic> GetTrafficByIp(string ip);
        Traffic Get(int id);
        void Add(Traffic newTraffic);
        void Update(Traffic newTraffic);
        void Delete(int id);
    }
}
