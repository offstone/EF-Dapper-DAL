using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using OffStone.Example.Dal.Entities;

namespace OffStone.Example.DAL.Migrations
{
    public class NorthwindContextFactory : IDesignTimeDbContextFactory<NorthwindContext>
    {
        public NorthwindContext CreateDbContext(string[] args)
        {
            var configBuilder = new ConfigurationBuilder();
            configBuilder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            var config = configBuilder.Build();

            var optionsBuilder = new DbContextOptionsBuilder<NorthwindContext>();
            optionsBuilder.UseSqlServer(config.GetConnectionString("NorthwindDBConnectionString"));
            return new NorthwindContext(optionsBuilder.Options);
        }
    }
}
