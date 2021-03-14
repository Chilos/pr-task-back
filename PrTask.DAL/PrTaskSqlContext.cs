using Microsoft.EntityFrameworkCore;
using PrTask.DAL.Domain;

namespace PrTask.DAL
{
    public sealed class PrTaskSqlContext: DbContext
    {
        internal DbSet<UserEnt> UserEnt { get; set; }
        
        public PrTaskSqlContext()
        {
            
        }
        
        public PrTaskSqlContext(DbContextOptions<PrTaskSqlContext> options): base(options)
        {
            ChangeTracker.AutoDetectChangesEnabled = false;
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost;Database=pr-task;User ID=sa; Password=Ffff050629!;");
            }
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserEnt>().HasKey(u => u.Id);
            builder.Entity<UserEnt>().Property(p => p.Id).ValueGeneratedOnAdd();
            base.OnModelCreating(builder);
        }
    }
}