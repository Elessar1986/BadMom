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
    class OperationTypeRepository : IRepository<OperationType>
    {
        private BadMomDB db;

        public OperationTypeRepository(BadMomDB context)
        {
            db = context;
        }

        public void Create(OperationType item)
        {
            db.OperationType.Add(item);
        }

        public void Delete(int id)
        {
            var item = db.OperationType.Find(id);
            if (item != null)
                db.OperationType.Remove(item);
        }

        public IEnumerable<OperationType> Find(Func<OperationType, bool> predicate)
        {
            return db.OperationType.Where(predicate).ToList();
        }

        public OperationType Get(int id)
        {
            return db.OperationType.Find(id);
        }

        public IEnumerable<OperationType> GetAll()
        {
            return db.OperationType;
        }

        public void Update(OperationType item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
