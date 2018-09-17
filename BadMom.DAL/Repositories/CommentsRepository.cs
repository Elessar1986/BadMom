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
    class CommentsRepository : IRepository<Comment>
    {
        private BadMomDB db;

        public CommentsRepository(BadMomDB context)
        {
            db = context;
        }

        public void Create(Comment item)
        {
            db.Comment.Add(item);
        }

        public void Delete(long id)
        {
            var item = db.Comment.Find(id);
            if (item != null)
                db.Comment.Remove(item);
        }

        public IEnumerable<Comment> Find(Func<Comment, bool> predicate)
        {
            return db.Comment.Include(c => c.Users).Where(predicate).ToList();
        }

        public Comment Get(long id)
        {
            return db.Comment.Find(id);
        }

        public IEnumerable<Comment> GetAll()
        {
            return db.Comment.Include(c => c.Users);
        }

        public void Update(Comment item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
