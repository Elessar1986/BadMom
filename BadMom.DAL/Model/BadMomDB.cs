namespace BadMom.DAL.Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class BadMomDB : DbContext
    {
        public BadMomDB(string connectionString)
            : base(connectionString)
        {
        }

        public virtual DbSet<Advert> Advert { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Comment> Comment { get; set; }
        public virtual DbSet<Consumption> Consumption { get; set; }
        public virtual DbSet<ConsumptionReason> ConsumptionReason { get; set; }
        public virtual DbSet<Events> Events { get; set; }
        public virtual DbSet<EventType> EventType { get; set; }
        public virtual DbSet<FavoriteAdvert> FavoriteAdvert { get; set; }
        public virtual DbSet<Income> Income { get; set; }
        public virtual DbSet<IncomeReason> IncomeReason { get; set; }
        public virtual DbSet<logEvents> logEvents { get; set; }
        public virtual DbSet<logTypes> logTypes { get; set; }
        public virtual DbSet<Messages> Messages { get; set; }
        public virtual DbSet<OperationType> OperationType { get; set; }
        public virtual DbSet<PersonalMessage> PersonalMessage { get; set; }
        public virtual DbSet<ResourceType> ResourceType { get; set; }
        public virtual DbSet<Source> Source { get; set; }
        public virtual DbSet<Themes> Themes { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Advert>()
                .HasMany(e => e.FavoriteAdvert)
                .WithRequired(e => e.Advert)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Category>()
                .HasMany(e => e.Advert)
                .WithRequired(e => e.Category1)
                .HasForeignKey(e => e.Category)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ConsumptionReason>()
                .HasMany(e => e.Consumption)
                .WithRequired(e => e.ConsumptionReason)
                .HasForeignKey(e => e.Reason)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EventType>()
                .Property(e => e.Color)
                .IsFixedLength();

            modelBuilder.Entity<EventType>()
                .HasMany(e => e.Events)
                .WithRequired(e => e.EventType)
                .HasForeignKey(e => e.Type)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<IncomeReason>()
                .HasMany(e => e.Income)
                .WithRequired(e => e.IncomeReason)
                .HasForeignKey(e => e.Reason)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<logTypes>()
                .HasMany(e => e.logEvents)
                .WithRequired(e => e.logTypes)
                .HasForeignKey(e => e.Type)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Messages>()
                .HasMany(e => e.Comment)
                .WithRequired(e => e.Messages)
                .HasForeignKey(e => e.MessageId);

            modelBuilder.Entity<Messages>()
                .HasMany(e => e.Users1)
                .WithMany(e => e.Messages1)
                .Map(m => m.ToTable("Likes").MapLeftKey("MessageId").MapRightKey("UserId"));

            modelBuilder.Entity<OperationType>()
                .HasMany(e => e.Consumption)
                .WithRequired(e => e.OperationType)
                .HasForeignKey(e => e.Type)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OperationType>()
                .HasMany(e => e.Income)
                .WithRequired(e => e.OperationType)
                .HasForeignKey(e => e.Type)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ResourceType>()
                .HasMany(e => e.Source)
                .WithRequired(e => e.ResourceType)
                .HasForeignKey(e => e.Type)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Source>()
                .HasMany(e => e.Consumption)
                .WithRequired(e => e.Source1)
                .HasForeignKey(e => e.Source)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Source>()
                .HasMany(e => e.Events)
                .WithOptional(e => e.Source1)
                .HasForeignKey(e => e.Source);

            modelBuilder.Entity<Source>()
                .HasMany(e => e.Income)
                .WithRequired(e => e.Source)
                .HasForeignKey(e => e.Destination)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Themes>()
                .HasMany(e => e.Messages)
                .WithRequired(e => e.Themes)
                .HasForeignKey(e => e.Theme)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Advert)
                .WithRequired(e => e.Users)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Comment)
                .WithRequired(e => e.Users)
                .HasForeignKey(e => e.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Consumption)
                .WithRequired(e => e.Users)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Events)
                .WithRequired(e => e.Users)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.FavoriteAdvert)
                .WithRequired(e => e.Users)
                .HasForeignKey(e => e.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Income)
                .WithRequired(e => e.Users)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Messages)
                .WithRequired(e => e.Users)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.PersonalMessage)
                .WithRequired(e => e.Users)
                .HasForeignKey(e => e.UserFrom)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.PersonalMessage1)
                .WithRequired(e => e.Users1)
                .HasForeignKey(e => e.UserTo)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Source)
                .WithRequired(e => e.Users)
                .HasForeignKey(e => e.UserId);
        }
    }
}
