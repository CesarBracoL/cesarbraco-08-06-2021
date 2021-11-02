using Domain.Common;
using Infrastructure.Logging;
using Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EndPoints>(configuration.GetSection(EndPoints.Name));
            
            services.AddScoped<IAppLogger, SerilogLogger>();

            services.AddSingleton<IAlbumService, AlbumService>();
            services.AddSingleton<IPhotoService, PhotoService>();
            services.AddSingleton<ICommentService, CommentService>();

            
        }
    }
}
