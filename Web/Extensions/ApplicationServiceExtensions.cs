using Application.Repositories;
using Application.Usecases.AddPlayerToDepthChart;
using Application.Usecases.GetBackups;
using Application.Usecases.GetFullDepthChart;
using Application.Usecases.RemovePlayerFromDepthChart;
using Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Web.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,
            IConfiguration config)
        {

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(config.GetConnectionString("DefaultConnection")));

            services.TryAddScoped<ITeamDepthChartRepo, TeamDepthChartRepo>();

            services.TryAddScoped<IPlayerRepo, PlayerRepo>();

            services.TryAddScoped<IAddPlayerToDepthChartUsecase, AddPlayerToDepthChartUsecase>();

            services.TryAddScoped<IRemovePlayerFromDepthChartUsecase, RemovePlayerToDepthChartUsecase>();

            services.TryAddScoped<IGetBackupsUsecase, GetBackupsUsecase>();

            services.TryAddScoped<IGetFullDepthChartUsecase, GetFullDepthChartUsecase>();

            return services;
        }
    }
}