using Api.Middlewares;
using Domain.Interfaces;
using Domain.Settings;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using IoC;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

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
            services.AddScoped(c => appSettings);

            //services
            //        .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //        .AddJwtBearer(c =>
            //        {
            //            c.Authority = "https://securetoken.google.com/graphcoutries";
            //            c.TokenValidationParameters = new TokenValidationParameters()
            //            {
            //                ValidateIssuer = true,
            //                ValidIssuer = "https://securetoken.google.com/graphcoutries",
            //                ValidateAudience = true,
            //                ValidAudience = "graphcoutries",
            //                ValidateLifetime = true
            //            };
            //        });

            //services
            //    .AddAuthentication(options => {
            //        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //        options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
            //    })
            //    .AddGoogle(options =>
            //    {
            //        IConfigurationSection googleAuthNSection =
            //            Configuration.GetSection("Authentication:Google");

            //        options.ClientId = googleAuthNSection["ClientId"];
            //        options.ClientSecret = googleAuthNSection["ClientSecret"];
            //    });

            services.AddCors(c =>
            {
                c.AddPolicy("CorsPolicy", builder => 
                builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });

            services
               .ConfigureContext(appSettings.ConnectionStringDefault)
               .ConfigureServices()
               .ConfigureRepositories();

            services
                .AddGraphQL()
                .AddGraphTypes(ServiceLifetime.Scoped);

            services
                .AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Graph Coutries Api", Version = "v1" });
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
                   .UseSwaggerUI(c =>
                   {
                       c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                   })
                   .UseGraphQLPlayground(options: new PlaygroundOptions());
            }
            else
            {
                app.UseHsts();
            }

            app
              .UseHttpsRedirection()
              .UseStaticFiles()
              .UseCors("CorsPolicy")
              .UseMiddleware(typeof(ErrorHandlingMiddleware))
              .UseRouting()
              .UseAuthentication()
              .UseAuthorization()
              .UseEndpoints(c =>
              {
                  c.MapControllers();
              });
        }
    }
}
