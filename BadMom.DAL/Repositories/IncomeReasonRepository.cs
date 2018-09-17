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
    class IncomeReasonRepository : IRepository<IncomeReason>
    {
        private BadMomDB db;

        public IncomeReasonRepository(BadMomDB context)
        {
            db = context;
        }

        public void Create(IncomeReason item)
        {
            db.IncomeReason.Add(item);
        }

        public void Delete(long id)
        {
            var item = db.IncomeReason.Find(id);
            if (item != null)
                db.IncomeReason.Remove(item);
        }

        public IEnumerable<IncomeReason> Find(Func<IncomeReason, bool> predicate)
        {
            return db.IncomeReason.Where(predicate).ToList();
        }

        public IncomeReason Get(long id)
        {
            return db.IncomeReason.Find(id);
        }

        public IEnumerable<IncomeReason> GetAll()
        {
            return db.IncomeReason;
        }

        public void Update(IncomeReason item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
