using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using EventsExpress.Core.ChatHub;
using EventsExpress.Core.Extensions;
using EventsExpress.Core.Infrastructure;
using EventsExpress.Core.IServices;
using EventsExpress.Core.NotificationHandlers;
using EventsExpress.Core.Services;
using EventsExpress.Db.EF;
using EventsExpress.Db.IRepo;
using EventsExpress.Db.Repo;
using EventsExpress.DTO;
using EventsExpress.Mapping;
using EventsExpress.Validation;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;

namespace EventsExpress
{
    /// <summary>
    /// The Startup class configures services and the app's request pipeline.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Gets represents a set of key/value application configuration properties.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        ///  This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        public void ConfigureServices(IServiceCollection services)
        {
            #region Authorization and Autontification configuring...

            var signingKey = new SigningSymmetricKey(Configuration.GetValue<string>("JWTOptions:SecretKey"));

            services.AddSingleton<IJwtSigningEncodingKey>(signingKey);
            services.AddTransient<ITokenService, TokenService>();

            var signingDecodingKey = (IJwtSigningDecodingKey)signingKey;

            services
                .AddMemoryCache()
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = signingDecodingKey.GetKey(),

                        ValidateIssuer = false,
                        ValidateAudience = false,

                        ValidateLifetime = true,

                        ClockSkew = TimeSpan.FromSeconds(5),
                    };
                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var accessToken = context.Request.Query["access_token"];

                            // If the request is for our hub...
                            var path = context.HttpContext.Request.Path;
                            if (!string.IsNullOrEmpty(accessToken) &&
                                path.StartsWithSegments("/chatRoom"))
                            {
                                // Read the token out of the query string
                                context.Token = accessToken;
                            }

                            return Task.CompletedTask;
                        },
                    };
                });

            #endregion

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(Configuration
                    .GetConnectionString("DefaultConnection")));

            #region Configure our services...

            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IEventService, EventService>();
            services.AddTransient<IEventStatusHistoryService, EventStatusHistoryService>();
            services.AddTransient<IMessageService, MessageService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<ICountryService, CountryService>();
            services.AddTransient<ICityService, CityService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<ICommentService, CommentService>();

            services.AddSingleton<ICacheHelper, CacheHelper>();
            services.AddTransient<IPhotoService, PhotoService>();
            services.Configure<ImageOptionsModel>(Configuration.GetSection("ImageWidths"));

            services.AddTransient<IEmailService, EmailService>();
            services.Configure<EmailOptionsModel>(Configuration.GetSection("EmailSenderOptions"));
            services.Configure<JwtOptionsModel>(Configuration.GetSection("JWTOptions"));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            #endregion
            services.AddCors();

            services.AddMvc().AddFluentValidation()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddTransient<IValidator<LoginDto>, LoginDtoValidator>();
            services.AddTransient<IValidator<ChangePasswordDto>, ChangePasswordDtoValidator>();
            services.AddTransient<IValidator<CategoryDto>, CategoryDtoValidator>();
            services.AddTransient<IValidator<CommentDto>, CommentDtoValidator>();
            services.AddTransient<IValidator<DTO.EventDto>, EventDtoValidator>();
            services.AddTransient<IValidator<AttitudeDto>, AttitudeDtoValidator>();
            services.AddTransient<IValidator<RateDto>, RateDtoValidator>();
            services.AddTransient<IValidator<EventStatusHistoryDto>, EventStatusHistoryDtoValidation>();

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });

            services.AddMediatR(typeof(EventCreatedHandler).Assembly);

            services.AddAutoMapper(typeof(AutoMapperProfile).GetTypeInfo().Assembly);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "EventsExpress API", Version = "v1" });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.XML";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                c.IncludeXmlComments(xmlPath);
            });
            services.AddSignalR();
            services.AddSingleton<IUserIdProvider, SignalRUserIdProvider>();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
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

            app.UseCors(x => x
               .AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader()
               .AllowCredentials());
            app.UseAuthentication();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            app.UseHttpContext();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });
            app.UseSignalR(routes =>
            {
                routes.MapHub<ChatRoom>("/chatRoom");
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
