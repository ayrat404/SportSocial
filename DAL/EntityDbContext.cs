using System.Data.Entity;
using DAL.DomainModel;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DAL
{
    public class EntityDbContext: IdentityDbContext<AppUser>
    {
    #if DEBUG 
        public EntityDbContext() : base("EntityDbContextDebug")
        {
        }
    #endif
    #if !DEBUG 
        public EntityDbContext() : base("EntityDbContextRelease")
        {
        }
    #endif
        public DbSet<SmsCode> SmsCodes { get; set; }

        static EntityDbContext()
        {
            Database.SetInitializer(new DbInit());
        }

        public static EntityDbContext Create()
        {
            return new EntityDbContext();
        }
    }

    public class DbInit: CreateDatabaseIfNotExists<EntityDbContext>
    {
        protected override void Seed(EntityDbContext context)
        {
            InitSetup(context);
            base.Seed(context);
        }

        private void InitSetup(EntityDbContext context)
        {
            //throw new System.NotImplementedException();
        }
    }
}