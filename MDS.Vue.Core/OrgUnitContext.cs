using Microsoft.EntityFrameworkCore;

namespace MDS.Vue.Core
{
    public class OrgUnitContext : DbContext
    {
        public OrgUnitContext(DbContextOptions<OrgUnitContext> options) : base(options) { }

        public OrgUnitContext() { }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=OrgUnit;Trusted_Connection=True;");
            //optionsBuilder.UseSqlServer(@"Server=tcp:mds-runchev.database.windows.net,1433;Initial Catalog=OrgUnit;Persist Security Info=False;User ID=runchev;Password=Metinve$t;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }

        public DbSet<OrgUnit> OrgUnits { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrgUnit>()
                .HasMany(e => e.Children)
                .WithOne(e => e.Parent)
                .HasForeignKey(e => e.ParentId);
        }
    }
}
