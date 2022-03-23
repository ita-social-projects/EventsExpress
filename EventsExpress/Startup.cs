using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using EventsExpress.Core.HostedService;
using EventsExpress.Core.Infrastructure;
using EventsExpress.Core.IServices;
using EventsExpress.Core.Services;
using EventsExpress.Db.Bridge;
using EventsExpress.Db.EF;
using EventsExpress.Filters;
using EventsExpress.Hubs;
using EventsExpress.Mapping;
using EventsExpress.NotificationHandlers;
using EventsExpress.Policies;
using EventsExpress.SwaggerSettings;
using EventsExpress.Validation;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;

namespace EventsExpress
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
            #region Authorization and Autontification configuring...

            var signingKey = new SigningSymmetricKey(Configuration.GetValue<string>("JWTOptions:SecretKey"));

            services.AddSingleton<IJwtSigningEncodingKey>(signingKey);
            services.AddTransient<ITokenService, TokenService>();

            var signingDecodingKey = (IJwtSigningDecodingKey)signingKey;

            services
                .AddMemoryCache()
                .AddAuthorization(options =>
                {
                    options.AddPolicy(PolicyNames.AdminPolicyName, policy =>
                        policy.Requirements.Add(new RoleRequirement(PolicyNames.AdminRole)));
                    options.AddPolicy(PolicyNames.UserPolicyName, policy =>
                        policy.Requirements.Add(new RoleRequirement(PolicyNames.UserRole)));
                    options.AddPolicy(PolicyNames.AdminAndUserPolicyName, policy =>
                        policy.RequireRole(PolicyNames.AdminRole, PolicyNames.UserRole));
                })
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
            services.AddSingleton<IAuthorizationHandler, RoleHandler>();

            #endregion

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection"),
                    x => x.UseNetTopologySuite()));

            #region Configure our services...
            services.AddScoped<ISecurityContext, SecurityContextService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<IEventScheduleManager, EventScheduleManager>();
            services.AddScoped<IEventStatusHistoryService, EventStatusHistoryService>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICategoryGroupService, CategoryGroupService>();
            services.AddScoped<ITrackService, TrackService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IInventoryService, InventoryService>();
            services.AddScoped<IUnitOfMeasuringService, UnitOfMeasuringService>();
            services.AddScoped<ICategoryOfMeasuringService, CategoryOfMeasuringService>();
            services.AddScoped<IUserEventInventoryService, UserEventInventoryService>();
            services.AddScoped<IEventOrganizersService, EventOrganizersService>();
            services.AddTransient<IGoogleSignatureVerificator, GoogleSignatureVerificator>();
            services.AddScoped<ITrackService, TrackService>();
            services.AddScoped<INotificationTemplateService, NotificationTemplateService>();
            services.AddScoped<IContactAdminService, ContactAdminService>();
            services.AddScoped<IIpProviderService, IpProviderService>();
            services.AddScoped<IBookmarkService, BookmarkService>();

            services.AddScoped<ILocationManager, LocationManager>();
            services.AddSingleton<ICacheHelper, CacheHelper>();
            services.AddScoped<IPhotoService, PhotoService>();
            services.AddScoped<IPasswordHasher, PasswordHasherService>();
            services.AddScoped<INotificationTypeService, NotificationTypeService>();
            services.Configure<ImageOptionsModel>(Configuration.GetSection("ImageWidths"));

            services.AddSingleton<IEmailService, EmailService>();
            services.Configure<EmailOptionsModel>(Configuration.GetSection("EmailSenderOptions"));
            services.Configure<JwtOptionsModel>(Configuration.GetSection("JWTOptions"));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.Configure<AppBaseUrlModel>(Configuration.GetSection("AppBaseUrl"));

            services.AddSingleton<UserAccessTypeFilterAttribute>();
            services.AddHostedService<SendMessageHostedService>();
            #endregion
            services.AddCors(options =>
                options.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins(Configuration.GetSection("AllowedOrigins")
                                                     .Get<string[]>())
                           .AllowAnyMethod()
                           .AllowAnyHeader()
                           .AllowCredentials();
                }));
            services.AddControllers();
            services.AddHttpClient();
            services.Configure<ViewModels.FrontConfigsViewModel>(Configuration.GetSection("FrontEndConfigs"));
            services.AddAzureClients(builder =>
            {
                // Add a storage account client
                builder.AddBlobServiceClient(Configuration.GetConnectionString("AzureBlobConnection"));
            });

            services.AddMvc().AddFluentValidation(options =>
            {
                options.RegisterValidatorsFromAssemblyContaining<Startup>();
                ValidatorOptions.Global.PropertyNameResolver = (_, memberInfo, expression) =>
                    CamelCasePropertyNameResolver.ResolvePropertyName(memberInfo, expression);
            }).AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            });

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });

            services.AddMediatR(typeof(OwnEventChangedHandler).Assembly);

            services.AddAutoMapper(typeof(UserMapperProfile).GetTypeInfo().Assembly);

            services.AddControllersWithViews(options =>
            {
                options.Filters.Add(typeof(EventsExpressExceptionFilterAttribute));
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "EventsExpress API", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                });

                c.AddSecurityRequirement(
                    new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer",
                                },
                                Scheme = "oauth2",
                                Name = "Bearer",
                                In = ParameterLocation.Header,
                            },
                            new List<string>()
                        },
                    });

                c.DocumentFilter<ApplyDocumentExtension>();
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                c.IncludeXmlComments(xmlPath);
            });

            services.AddFluentValidationRulesToSwagger();
            services.AddSwaggerGenNewtonsoftSupport();
            services.AddSignalR();
            services.AddSingleton<IUserIdProvider, SignalRUserIdProvider>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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

            var supportedCultures = new[]
            {
                new CultureInfo("en-US"),
            };
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en-US"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures,
            });

            app.UseCors();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller}/{action=Index}/{id?}");
                endpoints.MapHub<ChatRoom>("/chatRoom");
                endpoints.MapHub<UsersHub>("/usersHub");
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "EventsExpress API");
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
