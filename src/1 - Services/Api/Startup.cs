using Api.Middlewares;
using Domain.Interfaces;
using Domain.Settings;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using IoC;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            AppSettings appSettings = new AppSettings(Configuration);
            services.AddScoped(options => appSettings);

            services
                .ConfigureContext(appSettings.ConnectionStringDefault)
                .ConfigureServices()
                .ConfigureRepositories();

            services
                .AddGraphQL()
                .AddGraphTypes(ServiceLifetime.Scoped);

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder => 
                    builder
                        .WithOrigins("http://localhost:4200", "http://localhost:8081")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials()
                );
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = appSettings.JWTSettings.ValidIssuer,
                    ValidAudience = appSettings.JWTSettings.ValidAudience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings.JWTSettings.SecurityKey))
                };
            });

            services
                .AddControllers();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Graph Coutries Api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.Use(async (context, next) =>
            {
                //Request
                await next.Invoke();
                //Response
                var unitOfWork = (IUnitOfWork)context.RequestServices.GetService(typeof(IUnitOfWork));
                await unitOfWork.Commit();
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger()
                   .UseSwaggerUI(options =>
                   {
                       options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                   })
                   .UseGraphQLPlayground(options: new PlaygroundOptions());
            }
            else
            {
                app.UseHsts();
            }

            app
              .UseStaticFiles()
              .UseRouting()
              .UseCors("CorsPolicy")
              .UseMiddleware(typeof(ErrorHandlingMiddleware)) 
              .UseHttpsRedirection()
              .UseAuthentication()
              .UseAuthorization()
              .UseEndpoints(options =>
              {
                  options.MapControllers();
              });
        }
    }
}
