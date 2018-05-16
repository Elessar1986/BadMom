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
    class EventsRepository : IRepository<Events>
    {
        private BadMomDB db;

        public EventsRepository(BadMomDB context)
        {
            db = context;
        }

        public void Create(Events item)
        {
            db.Events.Add(item);
        }

        public void Delete(int id)
        {
            var item = db.Events.Find(id);
            if (item != null)
                db.Events.Remove(item);
        }

        public IEnumerable<Events> Find(Func<Events, bool> predicate)
        {
            return db.Events.Include(o => o.Source1).Include(c => c.EventType).Where(predicate).ToList();
        }

        public Events Get(int id)
        {
            return db.Events.Find(id);
        }

        public IEnumerable<Events> GetAll()
        {
            return db.Events.Include(o => o.Source1).Include(c => c.EventType);
        }

        public void Update(Events item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
