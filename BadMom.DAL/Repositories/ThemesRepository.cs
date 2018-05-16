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
    class ThemesRepository : IRepository<Themes>
    {
        private BadMomDB db;

        public ThemesRepository(BadMomDB context)
        {
            db = context;
        }

        public void Create(Themes item)
        {
            db.Themes.Add(item);
        }

        public void Delete(int id)
        {
            var item = db.Themes.Find(id);
            if (item != null)
                db.Themes.Remove(item);
        }

        public IEnumerable<Themes> Find(Func<Themes, bool> predicate)
        {
            return db.Themes.Where(predicate).ToList();
        }

        public Themes Get(int id)
        {
            return db.Themes.Find(id);
        }

        public IEnumerable<Themes> GetAll()
        {
            return db.Themes;
        }

        public void Update(Themes item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
