
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using TaskManager.API.Data.Configurations;
using TaskManager.API.Data.Repositories;

namespace TaskManager.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

        }
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            
            //instância unica da configuraçao da base de dados
            services.AddSingleton<IDatabaseConfig>(Configuration.GetSection(nameof(DatabaseConfig)).Get<DatabaseConfig>());

            //injeçao de dependencia 
            services.AddSingleton<ITarefasRepository, TarefasRepositoryMongo>();
            services.AddSingleton<IUsuariosRepository, UsuariosRepositoryMongo>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TaskManager.API", Version = "v1" });
            });

            services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
            {
                builder
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
                .WithOrigins("http://localhost:4200");
            }));

        }

        public void Configure(WebApplication app, IWebHostEnvironment environment)
        {
            if (environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TaskManager.API v1"));
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors("CorsPolicy");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });



        }
    }
}
