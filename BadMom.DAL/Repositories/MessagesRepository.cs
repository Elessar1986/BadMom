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
    class MessagesRepository : IRepository<Messages>
    {
        private BadMomDB db;

        public MessagesRepository(BadMomDB context)
        {
            db = context;
        }

        public void Create(Messages item)
        {
            db.Messages.Add(item);
        }

        public void Delete(int id)
        {
            var item = db.Messages.Find(id);
            if (item != null)
                db.Messages.Remove(item);
        }

        public IEnumerable<Messages> Find(Func<Messages, bool> predicate)
        {
            return db.Messages.Include(o => o.Comment).Include(c => c.Users1).Include(c => c.Themes).Include(c => c.Users).Where(predicate).ToList();
        }

        public Messages Get(int id)
        {
            return db.Messages.Find(id);
        }

        public IEnumerable<Messages> GetAll()
        {
            return db.Messages.Include(o => o.Comment).Include(c => c.Users1).Include(c => c.Themes).Include(c => c.Users);
        }

        public void Update(Messages item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
