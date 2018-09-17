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
    class PersonalMessageRepository : IRepository<PersonalMessage>
    {
        private BadMomDB db;

        public PersonalMessageRepository(BadMomDB context)
        {
            db = context;
        }

        public void Create(PersonalMessage item)
        {
            db.PersonalMessage.Add(item);
        }

        public void Delete(long id)
        {
            var item = db.PersonalMessage.Find(id);
            if (item != null)
                db.PersonalMessage.Remove(item);
        }

        public IEnumerable<PersonalMessage> Find(Func<PersonalMessage, bool> predicate)
        {
            return db.PersonalMessage.Where(predicate).ToList();
        }

        public PersonalMessage Get(long id)
        {
            return db.PersonalMessage.Find(id);
        }

        public IEnumerable<PersonalMessage> GetAll()
        {
            return db.PersonalMessage;
        }

        public void Update(PersonalMessage item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
