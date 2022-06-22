using ArtSpawn.Infrastructure;
using ArtSpawn.Infrastructure.Interfaces;
using ArtSpawn.Models.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace ArtSpawn.Extensions
{
    public static class DIExtensions
    {
        public static void ConfigureLoggerService(this IServiceCollection services) =>
            services.AddScoped<ILoggerService, LoggerService>();

        public static void ConfigureArtistService(this IServiceCollection services) =>
            services.AddScoped<IArtistService, ArtistService>();

    }
}
