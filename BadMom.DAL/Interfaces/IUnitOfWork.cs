using BadMom.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadMom.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Advert> Advert { get; }
        IRepository<Category> Category { get; }
        IRepository<Comment> Comment { get; }
        IRepository<Consumption> Consumption { get; }
        IRepository<ConsumptionReason> ConsumptionReason { get; }
        IRepository<Events> Events { get; }
        IRepository<EventType> EventType { get; }
        IRepository<Income> Income { get; }
        IRepository<IncomeReason> IncomeReason { get; }
        IRepository<logEvents> logEvents { get; }
        IRepository<logTypes> logTypes { get; }
        IRepository<Messages> Messages { get; }
        IRepository<OperationType> OperationType { get; }
        IRepository<ResourceType> ResourceType { get; }
        IRepository<Source> Source { get; }
        IRepository<Themes> Themes { get; }
        IRepository<Users> Users { get; }
        IRepository<PersonalMessage> PersonalMessage { get; }
        IRepository<FavoriteAdvert> FavoriteAdvert { get; }

        void Save();
    }
}
