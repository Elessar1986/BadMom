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
    class UsersRepository : IRepository<Users>
    {
        private BadMomDB db;

        public UsersRepository(BadMomDB context)
        {
            db = context;
        }

        public void Create(Users item)
        {
            db.Users.Add(item);
        }

        public void Delete(int id)
        {
            var item = db.Users.Find(id);
            if (item != null)
                db.Users.Remove(item);
        }

        public IEnumerable<Users> Find(Func<Users, bool> predicate)
        {
            return db.Users.Where(predicate).ToList();
        }

        public Users Get(int id)
        {
            return db.Users.Find(id);
        }

        public IEnumerable<Users> GetAll()
        {
            return db.Users;
        }

        public void Update(Users item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
