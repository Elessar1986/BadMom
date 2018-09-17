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
    class logTypesRepository : IRepository<logTypes>
    {
        private BadMomDB db;

        public logTypesRepository(BadMomDB context)
        {
            db = context;
        }

        public void Create(logTypes item)
        {
            db.logTypes.Add(item);
        }

        public void Delete(long id)
        {
            var item = db.logTypes.Find(id);
            if (item != null)
                db.logTypes.Remove(item);
        }

        public IEnumerable<logTypes> Find(Func<logTypes, bool> predicate)
        {
            return db.logTypes.Where(predicate).ToList();
        }

        public logTypes Get(long id)
        {
            return db.logTypes.Find(id);
        }

        public IEnumerable<logTypes> GetAll()
        {
            return db.logTypes;
        }

        public void Update(logTypes item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
