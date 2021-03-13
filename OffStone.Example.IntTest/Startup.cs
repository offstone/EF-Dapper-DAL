using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OffStone.Example.Dal.Entities;
using OffStone.Example.Dal.Repositories;
using OffStone.Example.DAL.Repositories;

namespace OffStone.Example.IntTest
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public static ServiceProvider ConfigureServices()
        {
            var configBuilder = new ConfigurationBuilder();
            configBuilder.AddJsonFile("appsettings.json", 
                optional: true, reloadOnChange: true);
            var config = configBuilder.Build();

            var services = new ServiceCollection();
            var startup = new Startup(config);
            startup.ConfigureServices(services);

            return services.BuildServiceProvider();
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<NorthwindContext>(options =>
            {
                options.UseSqlServer(
                    Configuration["ConnectionStrings:NorthwindDBConnectionString"]);
            });

            // register the generic and specific repositories
            services.AddScoped<IGenericRepository<Category>, GenericRepository<Category>>();
            services.AddScoped<IGenericRepository<Order>, GenericRepository<Order>>();
            services.AddScoped<IOrderRepository, OrderRepository>();
        }
    }
}
