using BadMom.DAL.Interfaces;
using BadMom.DAL.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadMom.DAL.Repositories
{
    class ResourceTypeRepository : IRepository<ResourceType>
    {
        private BadMomDB db;

        public ResourceTypeRepository(BadMomDB context)
        {
            db = context;
        }

        public void Create(ResourceType item)
        {
            db.ResourceType.Add(item);
        }

        public void Delete(long id)
        {
            var item = db.ResourceType.Find(id);
            if (item != null)
                db.ResourceType.Remove(item);
        }

        public IEnumerable<ResourceType> Find(Func<ResourceType, bool> predicate)
        {
            return db.ResourceType.Where(predicate).ToList();
        }

        public ResourceType Get(long id)
        {
            return db.ResourceType.Find(id);
        }

        public IEnumerable<ResourceType> GetAll()
        {
            return db.ResourceType;
        }

        public void Update(ResourceType item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
