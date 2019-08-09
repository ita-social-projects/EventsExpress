using EventsExpress.Core.Infrastructure;
using EventsExpress.Core.IServices;
using EventsExpress.Core.Services;
using EventsExpress.Db.EF;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using EventsExpress.Db.IRepo;
using EventsExpress.Db.Repo;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using AutoMapper;
using EventsExpress.Mapping;
using System.Reflection;
using MediatR;
using EventsExpress.Core.NotificationHandlers;

namespace EventsExpress
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
            #region Authorization and Autontification configuring...

            var signingKey = new SigningSymmetricKey(Configuration.GetValue<string>("JWTSecretKey"));

            services.AddSingleton<IJwtSigningEncodingKey>(signingKey);

            var signingDecodingKey = (IJwtSigningDecodingKey)signingKey;

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = signingDecodingKey.GetKey(),

                        ValidateIssuer = false,
                        ValidateAudience = false,

                        ValidateLifetime = true,

                        ClockSkew = TimeSpan.FromSeconds(5)
                    };
                });

            #endregion

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(Configuration
                    //.GetConnectionString("AzureSQLServer")));
                    .GetConnectionString("DefaultConnection")));

            #region Configure our services...

            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddTransient<IAuthServicre, AuthServicre>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IEventService, EventService>();

            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<ICountryService, CountryService>();
            services.AddTransient<ICityService, CityService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IPhotoService, PhotoService> ();
            services.AddTransient<ICommentService, CommentService>();
            


            #endregion

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });

            //services.AddMediatR(typeof(Startup));
            services.AddMediatR(typeof(EventCreatedHandler).Assembly);

            services.AddAutoMapper(typeof(AutoMapperProfile).GetTypeInfo().Assembly);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseAuthentication();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }
    }
}
