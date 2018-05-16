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
    class logEventsRepository : IRepository<logEvents>
    {
        private BadMomDB db;

        public logEventsRepository(BadMomDB context)
        {
            db = context;
        }

        public void Create(logEvents item)
        {
            db.logEvents.Add(item);
        }

        public void Delete(int id)
        {
            var item = db.logEvents.Find(id);
            if (item != null)
                db.logEvents.Remove(item);
        }

        public IEnumerable<logEvents> Find(Func<logEvents, bool> predicate)
        {
            return db.logEvents.Include(o => o.logTypes).Where(predicate).ToList();
        }

        public logEvents Get(int id)
        {
            return db.logEvents.Find(id);
        }

        public IEnumerable<logEvents> GetAll()
        {
            return db.logEvents.Include(o => o.logTypes);
        }

        public void Update(logEvents item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
