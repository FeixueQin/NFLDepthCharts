using Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace Web.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,
            IConfiguration config)
        {

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(config.GetConnectionString("DefaultConnection")));

            return services;
        }
    }
}