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
    class ConsumptionReasonRepository : IRepository<ConsumptionReason>
    {
        private BadMomDB db;

        public ConsumptionReasonRepository(BadMomDB context)
        {
            db = context;
        }

        public void Create(ConsumptionReason item)
        {
            db.ConsumptionReason.Add(item);
        }

        public void Delete(long id)
        {
            var item = db.ConsumptionReason.Find(id);
            if (item != null)
                db.ConsumptionReason.Remove(item);
        }

        public IEnumerable<ConsumptionReason> Find(Func<ConsumptionReason, bool> predicate)
        {
            return db.ConsumptionReason.Where(predicate).ToList();
        }

        public ConsumptionReason Get(long id)
        {
            return db.ConsumptionReason.Find(id);
        }

        public IEnumerable<ConsumptionReason> GetAll()
        {
            return db.ConsumptionReason;
        }

        public void Update(ConsumptionReason item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

    }
}
