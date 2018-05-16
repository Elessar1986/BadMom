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
    class CategoryRepository : IRepository<Category>
    {
        private BadMomDB db;

        public CategoryRepository(BadMomDB context)
        {
            db = context;
        }

        public void Create(Category item)
        {
            db.Category.Add(item);
        }

        public void Delete(int id)
        {
            var item = db.Category.Find(id);
            if (item != null)
                db.Category.Remove(item);
        }

        public IEnumerable<Category> Find(Func<Category, bool> predicate)
        {
            return db.Category.Where(predicate).ToList();
        }

        public Category Get(int id)
        {
            return db.Category.Find(id);
        }

        public IEnumerable<Category> GetAll()
        {
            return db.Category;
        }

        public void Update(Category item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
