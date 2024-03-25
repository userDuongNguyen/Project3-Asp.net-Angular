using Contracts;
using Contracts.RepositoryInterfaces;
using Infrastructure;
using LoggerSevice;
using Microsoft.EntityFrameworkCore;
using Repository.Repository;

namespace AngularReProject.Server.ServiceExtension
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
        }
        public static void ConfigureLoggerService(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerManager, LoggerManager>();
        }
        public static void ConfigureRepositoryWrapper(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
        }

        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<RepositoryContext>(options => options.
            UseSqlServer(config.GetConnectionString("DataContext")
                ?? throw new InvalidOperationException("Connection string 'DataContext' not found.")));
        }
    }
}

