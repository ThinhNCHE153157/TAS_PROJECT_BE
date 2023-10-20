using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TAS.Data.Entities.Interfaces;

namespace TAS.Data.EF
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public AppDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(local);uid=sa;pwd=123;database=Db_Capstone_TAS;Encrypt=False");
            }

            base.OnConfiguring(optionsBuilder);
        }

        #region DbSet
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            var modified = ChangeTracker.Entries().Where(e => e.State == EntityState.Modified || e.State == EntityState.Added);

            foreach (EntityEntry item in modified)
            {
                var changedOrAddedItem = item.Entity as IDateTracking;
                if (changedOrAddedItem != null)
                {
                    if (item.State == EntityState.Added)
                    {
                        changedOrAddedItem.CreateDate = DateTime.UtcNow;
                        changedOrAddedItem.CreateUser = "TAS-System";
                    }
                    changedOrAddedItem.UpdateDate = DateTime.UtcNow;
                    changedOrAddedItem.UpdateUser = "TAS-System";
                }
            }
            return base.SaveChanges();
        }

        public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var modified = ChangeTracker.Entries().Where(e => e.State == EntityState.Modified || e.State == EntityState.Added || e.State == EntityState.Deleted);
            foreach (EntityEntry item in modified)
            {
                var changedOrAddedItem = item.Entity as IDateTracking;
                if (changedOrAddedItem != null)
                {
                    if (item.State == EntityState.Added)
                    {
                        changedOrAddedItem.CreateDate = DateTime.UtcNow;
                        changedOrAddedItem.CreateUser = "TAS-System";
                    }
                    changedOrAddedItem.UpdateDate = DateTime.UtcNow;
                    changedOrAddedItem.UpdateUser = "TAS-System";
                }
            }
            return await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

    }
}
