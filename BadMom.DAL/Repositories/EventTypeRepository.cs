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
    class EventTypeRepository : IRepository<EventType>
    {
        private BadMomDB db;

        public EventTypeRepository(BadMomDB context)
        {
            db = context;
        }

        public void Create(EventType item)
        {
            db.EventType.Add(item);
        }

        public void Delete(long id)
        {
            var item = db.EventType.Find(id);
            if (item != null)
                db.EventType.Remove(item);
        }

        public IEnumerable<EventType> Find(Func<EventType, bool> predicate)
        {
            return db.EventType.Where(predicate).ToList();
        }

        public EventType Get(long id)
        {
            return db.EventType.Find(id);
        }

        public IEnumerable<EventType> GetAll()
        {
            return db.EventType;
        }

        public void Update(EventType item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
