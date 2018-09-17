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
    class FavoriteAdvertRepository : IRepository<FavoriteAdvert>
    {
        private BadMomDB db;

        public FavoriteAdvertRepository(BadMomDB context)
        {
            db = context;
        }

        public void Create(FavoriteAdvert item)
        {
            db.FavoriteAdvert.Add(item);
        }

        public void Delete(long id)
        {
            var item = db.FavoriteAdvert.Find(id);
            if (item != null)
                db.FavoriteAdvert.Remove(item);
        }

        public IEnumerable<FavoriteAdvert> Find(Func<FavoriteAdvert, bool> predicate)
        {
            return db.FavoriteAdvert.Where(predicate).ToList();
        }

        public FavoriteAdvert Get(long id)
        {
            return db.FavoriteAdvert.Find(id);
        }

        public IEnumerable<FavoriteAdvert> GetAll()
        {
            return db.FavoriteAdvert;
        }

        public void Update(FavoriteAdvert item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
