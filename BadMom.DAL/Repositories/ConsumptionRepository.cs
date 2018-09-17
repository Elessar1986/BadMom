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
    class ConsumptionRepository : IRepository<Consumption>
    {
        private BadMomDB db;

        public ConsumptionRepository(BadMomDB context)
        {
            db = context;
        }

        public void Create(Consumption item)
        {
            db.Consumption.Add(item);
        }

        public void Delete(long id)
        {
            var item = db.Consumption.Find(id);
            if (item != null)
                db.Consumption.Remove(item);
        }

        public IEnumerable<Consumption> Find(Func<Consumption, bool> predicate)
        {
            return db.Consumption.Include(c => c.ConsumptionReason).Include(c => c.OperationType).Include(c => c.Source1).Where(predicate).ToList();
        }

        public Consumption Get(long id)
        {
            return db.Consumption.Find(id);
        }

        public IEnumerable<Consumption> GetAll()
        {
            return db.Consumption.Include(c => c.ConsumptionReason).Include(c => c.OperationType).Include(c => c.Source1);
        }

        public void Update(Consumption item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
