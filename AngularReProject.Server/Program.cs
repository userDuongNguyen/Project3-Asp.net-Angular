using AngularReProject.Server.ServiceExtension;
using Contracts;
using NLog;
using Profiles;

namespace AngularReProject.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var fileXmlContent = File.ReadAllText("./nlog.config.xml");
            LogManager.Setup().LoadConfigurationFromXml(fileXmlContent);
            builder.Services.ConfigureRepositoryWrapper();
            builder.Services.ConfigureSqlContext(builder.Configuration);
            // Add services to the container.
            builder.Services.ConfigureCors();
            builder.Services.ConfigureLoggerService();
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAutoMapper(typeof(MappingProfile));
            var app = builder.Build();

            app.UseDefaultFiles();
            app.UseStaticFiles();
            var logger = app.Services.GetRequiredService<ILoggerManager>();
            app.ConfigureExceptionHandler(logger);
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseCors("CorsPolicy");

            app.MapControllers();

            app.MapFallbackToFile("/index.html");

            app.Run();
        }
    }
}
