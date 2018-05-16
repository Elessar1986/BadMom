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
    class SourceRepository : IRepository<Source>
    {
        private BadMomDB db;

        public SourceRepository(BadMomDB context)
        {
            db = context;
        }

        public void Create(Source item)
        {
            db.Source.Add(item);
        }

        public void Delete(int id)
        {
            var item = db.Source.Find(id);
            if (item != null)
                db.Source.Remove(item);
        }

        public IEnumerable<Source> Find(Func<Source, bool> predicate)
        {
            return db.Source.Include(o => o.ResourceType).Where(predicate).ToList();
        }

        public Source Get(int id)
        {
            return db.Source.Find(id);
        }

        public IEnumerable<Source> GetAll()
        {
            return db.Source.Include(o => o.ResourceType);
        }

        public void Update(Source item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
