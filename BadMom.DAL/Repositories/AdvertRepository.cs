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
    class AdvertRepository : IRepository<Advert>
    {
        private BadMomDB db;

        public AdvertRepository(BadMomDB context)
        {
            db = context;
        }

        public void Create(Advert item)
        {
            db.Advert.Add(item);
        }

        public void Delete(int id)
        {
            var item = db.Advert.Find(id);
            if (item != null)
                db.Advert.Remove(item);
        }

        public IEnumerable<Advert> Find(Func<Advert, bool> predicate)
        {
            return db.Advert.Include(o => o.Category1).Include(c => c.Users).Where(predicate).ToList();
        }

        public Advert Get(int id)
        {
            return db.Advert.Find(id);
        }

        public IEnumerable<Advert> GetAll()
        {
            return db.Advert.Include(o => o.Category1).Include(c => c.Users);
        }

        public void Update(Advert item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
