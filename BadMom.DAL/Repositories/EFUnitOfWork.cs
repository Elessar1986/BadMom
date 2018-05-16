using BadMom.DAL.Interfaces;
using BadMom.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadMom.DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private BadMomDB db;
        private AdvertRepository advertRepository;
        private CategoryRepository categoryRepository;
        private CommentsRepository commentsRepository;
        private ConsumptionRepository consumptionRepository;
        private ConsumptionReasonRepository consumptionReasonRepository;
        private EventsRepository eventsRepository;
        private EventTypeRepository eventTypeRepository;
        private IncomeRepository incomeRepository;
        private IncomeReasonRepository incomeReasonRepository;
        private logEventsRepository logEventsRepository;
        private logTypesRepository logTypesRepository;
        private MessagesRepository messagesRepository;
        private OperationTypeRepository operationTypeRepository;
        private ResourceTypeRepository resourceTypeRepository;
        private SourceRepository sourceRepository;
        private ThemesRepository themesRepository;
        private UsersRepository usersRepository;


        public EFUnitOfWork(string connectionString)
        {
            db = new BadMomDB(connectionString);
        }
        public IRepository<Advert> Advert
        {
            get
            {
                if (advertRepository == null)
                    advertRepository = new AdvertRepository(db);
                return advertRepository;
            }
        }

        public IRepository<Category> Category
        {
            get
            {
                if (categoryRepository == null)
                    categoryRepository = new CategoryRepository(db);
                return categoryRepository;
            }
        }

        public IRepository<Comment> Comment
        {
            get
            {
                if (commentsRepository == null)
                    commentsRepository = new CommentsRepository(db);
                return commentsRepository;
            }
        }


        public IRepository<Consumption> Consumption
        {
            get
            {
                if (consumptionRepository == null)
                    consumptionRepository = new ConsumptionRepository(db);
                return consumptionRepository;
            }
        }

        public IRepository<ConsumptionReason> ConsumptionReason
        {
            get
            {
                if (consumptionReasonRepository == null)
                    consumptionReasonRepository = new ConsumptionReasonRepository(db);
                return consumptionReasonRepository;
            }
        }

        public IRepository<Events> Events
        {
            get
            {
                if (eventsRepository == null)
                    eventsRepository = new EventsRepository(db);
                return eventsRepository;
            }
        }

        public IRepository<EventType> EventType
        {
            get
            {
                if (eventTypeRepository == null)
                    eventTypeRepository = new EventTypeRepository(db);
                return eventTypeRepository;
            }
        }
        public IRepository<Income> Income
        {
            get
            {
                if (incomeRepository == null)
                    incomeRepository = new IncomeRepository(db);
                return incomeRepository;
            }
        }

        public IRepository<IncomeReason> IncomeReason
        {
            get
            {
                if (incomeReasonRepository == null)
                    incomeReasonRepository = new IncomeReasonRepository(db);
                return incomeReasonRepository;
            }
        }

        public IRepository<logEvents> logEvents
        {
            get
            {
                if (logEventsRepository == null)
                    logEventsRepository = new logEventsRepository(db);
                return logEventsRepository;
            }
        }

        public IRepository<logTypes> logTypes
        {
            get
            {
                if (logTypesRepository == null)
                    logTypesRepository = new logTypesRepository(db);
                return logTypesRepository;
            }
        }

        public IRepository<Messages> Messages
        {
            get
            {
                if (messagesRepository == null)
                    messagesRepository = new MessagesRepository(db);
                return messagesRepository;
            }
        }

        public IRepository<OperationType> OperationType
        {
            get
            {
                if (operationTypeRepository == null)
                    operationTypeRepository = new OperationTypeRepository(db);
                return operationTypeRepository;
            }
        }

        public IRepository<ResourceType> ResourceType
        {
            get
            {
                if (resourceTypeRepository == null)
                    resourceTypeRepository = new ResourceTypeRepository(db);
                return resourceTypeRepository;
            }
        }

        public IRepository<Source> Source
        {
            get
            {
                if (sourceRepository == null)
                    sourceRepository = new SourceRepository(db);
                return sourceRepository;
            }
        }

        public IRepository<Themes> Themes
        {
            get
            {
                if (themesRepository == null)
                    themesRepository = new ThemesRepository(db);
                return themesRepository;
            }
        }

        public IRepository<Users> Users
        {
            get
            {
                if (usersRepository == null)
                    usersRepository = new UsersRepository(db);
                return usersRepository;
            }
        }


        public void Save()
        {
            db.SaveChanges();
        }

        #region Dispose

        

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion Dispose
    }
}
