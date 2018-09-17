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
    class IncomeRepository : IRepository<Income>
    {
        private BadMomDB db;

        public IncomeRepository(BadMomDB context)
        {
            db = context;
        }

        public void Create(Income item)
        {
            db.Income.Add(item);
        }

        public void Delete(long id)
        {
            var item = db.Income.Find(id);
            if (item != null)
                db.Income.Remove(item);
        }

        public IEnumerable<Income> Find(Func<Income, bool> predicate)
        {
            return db.Income.Include(o => o.IncomeReason).Include(c => c.OperationType).Include(c => c.Source).Include(c => c.Users).Where(predicate).ToList();
        }

        public Income Get(long id)
        {
            return db.Income.Find(id);
        }

        public IEnumerable<Income> GetAll()
        {
            return db.Income.Include(o => o.IncomeReason).Include(c => c.OperationType).Include(c => c.Source).Include(c => c.Users);
        }

        public void Update(Income item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
