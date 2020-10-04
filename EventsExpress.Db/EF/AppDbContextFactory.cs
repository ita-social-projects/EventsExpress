using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace EventsExpress.Db.EF
{
    /// <summary>
    /// ContextFactory needs for migration DB
    /// </summary>
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=EventsExpress;Trusted_Connection=True;MultipleActiveResultSets=true");
            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
