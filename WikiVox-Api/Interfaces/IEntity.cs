using System.Collections.Generic;
using Wikivox_Api.Models;

namespace Wikivox_Api.Interfaces
{
    public interface IEntity
    {
        IEnumerable<Entity> GetAll();
        Entity Get(int id);
        void Add(Entity newEntity);
        void Update(Entity newEntity);
        void Delete(int id);
    }
}
