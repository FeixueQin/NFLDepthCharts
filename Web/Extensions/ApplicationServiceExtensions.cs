using Application.Repositories;
using Application.Usecases.AddPlayerToDepthChart;
using Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace Web.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,
            IConfiguration config)
        {

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(config.GetConnectionString("DefaultConnection")));

            // services.AddSwaggerGen(c =>
            // {
            //     c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
                
            //     // Register the examples operation filter
            //     c.ExampleFilters();
            // });

            services.TryAddScoped<ITeamDepthChartRepo, TeamDepthChartRepo>();

            services.TryAddScoped<IAddPlayerToDepthChartUsecase, AddPlayerToDepthChartUsecase>();

            return services;
        }
    }
}