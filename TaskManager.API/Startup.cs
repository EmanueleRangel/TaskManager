
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using TaskManager.API.Data.Configurations;
using TaskManager.API.Data.Repositories;
using TaskManager.API.Health;
using TaskManager.API.Services.Tarefas;
using TaskManager.API.Services.Usuarios;

namespace TaskManager.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

        }
        public IConfiguration Configuration { get; }
        public object UIFramework { get; private set; }

        public void ConfigureServices(IServiceCollection services)
        {

            //instância unica da configuraçao da base de dados
            services.AddSingleton<IDatabaseConfig>(Configuration.GetSection(nameof(DatabaseConfig)).Get<DatabaseConfig>());

            //injeçao de dependencia 
            services.AddSingleton<ITarefasRepository, TarefasRepositoryMongo>();
            services.AddSingleton<IUsuariosRepository, UsuariosRepositoryMongo>();
            services.AddSingleton<ITarefasService, TarefasService>();
            services.AddSingleton<IUsuariosService, UsuariosService>();

            services.AddHealthChecks()
                .AddSqlServer(
                connectionString: Configuration["DatabaseConfig:ConnectionStringSQL"],
                name:"SQL DB", tags: new string[] { "database", "sql" })
                .AddMongoDb(
                mongodbConnectionString: Configuration["DatabaseConfig:ConnectionStringMongo"],
                name: "MongoDB", tags: new string[] { "database", "nosql", "mongodb" });


            services.AddHealthChecksUI(options =>
            {
                options.SetEvaluationTimeInSeconds(5);
                options.MaximumHistoryEntriesPerEndpoint(10);
                options.AddHealthCheckEndpoint("Infraestrutura", "/health-data-ui");
            })
                .AddInMemoryStorage();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                var securitySchemeApiKey = new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.ApiKey,
                    In = ParameterLocation.Header,
                    Name = "api_key",
                    Reference = new OpenApiReference
                    {
                        Id = "ApiKey",
                        Type = ReferenceType.SecurityScheme
                    }
                };

                var securitySchemeJWT = new OpenApiSecurityScheme
                {
                    Scheme = JwtBearerDefaults.AuthenticationScheme,
                    BearerFormat = "JWT",
                    Name = "JWT Authentication",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };

                c.AddSecurityDefinition(securitySchemeApiKey.Reference.Id, securitySchemeApiKey);
                
                c.AddSecurityDefinition(securitySchemeJWT.Reference.Id, securitySchemeJWT);

                var securityRequirement = new OpenApiSecurityRequirement
                {
                    {securitySchemeApiKey, Array.Empty<string>() },
                    {securitySchemeJWT, Array.Empty<string>() }
                };

                c.AddSecurityRequirement(securityRequirement);

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

            var key = Encoding.ASCII.GetBytes(Settings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
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

            app.UseHealthChecks(
                HealthChecker.Path,
                new HealthCheckOptions() { ResponseWriter = HealthChecker.WriteHealthResponse}
                );

            app.UseHealthChecks("/health-data-ui", new HealthCheckOptions
            {
                Predicate = p => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            app.UseHealthChecksUI(options => { options.UIPath = "/monitor"; });
        }

    }
}

