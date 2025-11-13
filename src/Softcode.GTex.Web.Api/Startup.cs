using Autofac;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Serilog;
using Softcode.GTex.Api;
using Softcode.GTex.Api.Crm;
using Softcode.GTex.Api.Dms;
using Softcode.GTex.Api.Hrm;
using Softcode.GTex.Api.Messaging;
using Softcode.GTex.ApploicationService;
using Softcode.GTex.Data;
using Softcode.GTex.Data.Models;
using Softcode.GTex.Web.Api.ApplicationService;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore;
using Hangfire;
using IdentityServer4.Stores;
using IdentityServer4.EntityFramework.Stores;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Identity;

namespace Softcode.GTex.Web.Api
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            //services.Configure<CookiePolicyOptions>(options =>
            //{
            //    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
            //    options.CheckConsentNeeded = context => false;
            //    options.MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.None;
            //});


            string migrationAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
            services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1).AddJsonOptions(options =>
            {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.DefaultValueHandling = DefaultValueHandling.Include;
                options.SerializerSettings.NullValueHandling = NullValueHandling.Include;

                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Serialize;
                options.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.None;

            });
            services.AddAutoMapper();

            services.AddMvcCore(o => { o.Filters.Add<CustomExceptionFilter>(); }).AddApplicationPart(Assembly.Load(new AssemblyName("Softcode.GTex.Api"))).AddControllersAsServices();
            services.AddMvcCore(o => { o.Filters.Add<CustomExceptionFilter>(); }).AddApplicationPart(Assembly.Load(new AssemblyName("Softcode.GTex.Api.Hrm"))).AddControllersAsServices();
            services.AddMvcCore(o => { o.Filters.Add<CustomExceptionFilter>(); }).AddApplicationPart(Assembly.Load(new AssemblyName("Softcode.GTex.Api.Crm"))).AddControllersAsServices();
            services.AddMvcCore(o => { o.Filters.Add<CustomExceptionFilter>(); }).AddApplicationPart(Assembly.Load(new AssemblyName("Softcode.GTex.Api.Dms"))).AddControllersAsServices();
            services.AddMvcCore(o => { o.Filters.Add<CustomExceptionFilter>(); }).AddApplicationPart(Assembly.Load(new AssemblyName("Softcode.GTex.Api.Messaging"))).AddControllersAsServices();

            services.AddDistributedMemoryCache(); // Adds a default in-memory implementation of IDistributedCache

            //services.AddSession(options =>
            //{
            //    // Set a short timeout for easy testing.
            //    options.IdleTimeout = TimeSpan.FromMinutes(1);
            //    options.Cookie.HttpOnly = true;
            //});


            services.Configure<ApiBehaviorOptions>(opt =>
            {
                opt.SuppressModelStateInvalidFilter = true;
            });

            MapperConfiguration mapperConfiguration = new MapperConfiguration(config =>
            {
                config.AddProfile<ApplicationDataMappingProfile>();
                config.AddProfile<ApplicationServiceMappingProfile>();
                config.AddProfile<ApiDataMappingProfile>();
                config.AddProfile<ApiServiceMappingProfile>();
                config.AddProfile<DmsServiceMappingProfile>();
                config.AddProfile<DmsDataMappingProfile>();
                config.AddProfile<CrmServiceMappingProfile>();
                config.AddProfile<CrmDataMappingProfile>();
                config.AddProfile<MessagingDataMappingProfile>();
                config.AddProfile<MessagingServiceMappingProfile>();
                config.AddProfile<HrmDataMappingProfile>();
                config.AddProfile<HrmServiceMappingProfile>();

            });

            mapperConfiguration.CreateMapper();

            services.AddSingleton(mapperConfiguration);


            #region Identity Server Configuration

            string[] applicationAllowedUrls = ItmConfigurations.ApplicationAllowedUrls;

            services.AddCors();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.WithOrigins(applicationAllowedUrls)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
                    );

                options.AddPolicy("AllowHeaders",
                    builder =>
                    {
                        builder.WithOrigins(applicationAllowedUrls)
                               .WithHeaders(HeaderNames.ContentType, "x-custom-header", HeaderNames.ContentDisposition);
                    });
            });


            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 0;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;

                options.Tokens.ProviderMap.Add("Default", new TokenProviderDescriptor(typeof(IUserTwoFactorTokenProvider<ApplicationUser>)));
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            services.Configure<DataProtectionTokenProviderOptions>(options =>
            {
                options.Name = "Default";
                options.TokenLifespan = TimeSpan.FromHours(1);
            });



            //services.AddTransient<IProfileService, AdditionalClaimsProfileService>();
            services.AddIdentityServer()
                //.AddConfigurationStore(options =>
                //{
                //    options.ConfigureDbContext = builder =>
                //        builder.UseSqlServer(ItmConfigurations.ApplicationConnectionString,
                //            sql => sql.MigrationsAssembly(migrationAssembly));
                //})
                // this adds the operational data from DB (codes, tokens, consents)
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = builder =>
                        builder.UseSqlServer(ItmConfigurations.ApplicationConnectionString,
                            sql => sql.MigrationsAssembly(migrationAssembly));

                    // this enables automatic token cleanup. this is optional.
                    options.EnableTokenCleanup = true;
                    options.TokenCleanupInterval = 10;
                    options.DefaultSchema = "security";
                })
                .AddDeveloperSigningCredential()
                .AddAspNetIdentity<ApplicationUser>()
                .AddProfileService<AdditionalClaimsProfileService>()
                .AddJwtBearerClientAuthentication()
                .AddAppAuthRedirectUriValidator()
                .AddInMemoryClients(IdentityServerConfiguration.GetClients())
                .AddInMemoryIdentityResources(IdentityServerConfiguration.GetIdentityResources())
                .AddInMemoryApiResources(IdentityServerConfiguration.GetApiResources())
                ;

            services.AddTransient<IPersistedGrantStore, PersistedGrantStore>();

            services.AddAuthentication()
                .AddJwtBearer(jwt =>
                {
                    jwt.Authority = ItmConfigurations.TokenAuthority;
                    jwt.Audience = ItmConfigurations.TokenAudience;
                    jwt.RequireHttpsMetadata = false;
                }).AddCookie(options =>
                {
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(1);
                    options.SlidingExpiration = true;

                });
            //.Services.ConfigureApplicationCookie(options => {
            //    options.ExpireTimeSpan = TimeSpan.FromMinutes(1);
            //    options.SlidingExpiration = true;
            //});

            #endregion

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v6.0.0",
                    Title = "It Magnet API service",
                    Contact = new Swashbuckle.AspNetCore.Swagger.Contact() { Name = "It Magnet", Email = "support@itmagnet.com.au", Url = "http://itmagnet.com.au" }
                });

                string filePath = Path.Combine(System.AppContext.BaseDirectory, "Softcode.GTex.Web.Api.xml");
                c.IncludeXmlComments(filePath);

                filePath = Path.Combine(System.AppContext.BaseDirectory, "Softcode.GTex.Api.Hrm.xml");
                c.IncludeXmlComments(filePath);

            });

            services.AddHangfire(x => x.UseSqlServerStorage(ItmConfigurations.HangfireConnectionString));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        public void ConfigureContainer(ContainerBuilder builder)
        {
            // Add any Autofac modules or registrations.
            // This is called AFTER ConfigureServices so things you
            // register here OVERRIDE things registered in ConfigureServices.
            //
            // You must have the call to AddAutofac in the Program.Main
            // method or this won't be called.
            builder.RegisterModule(new ApplicationAutofacModule());
            builder.RegisterModule(new ApiAutofacModule());
            builder.RegisterModule(new CrmAutofacModule());
            builder.RegisterModule(new DmsAutofacModule());
            builder.RegisterModule(new MessagingAutofacModule());
            builder.RegisterModule(new HrmAutofacModule());
        }

        /// <summary>
        /// This method gets called by the runtime.Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="environment"></param>
        /// <param name="serviceProvider"></param>
        /// <param name="hangfireService"></param>
        /// <param name="loggerFactory"></param>
        public void Configure(IApplicationBuilder app
            , IHostingEnvironment environment
            , IServiceProvider serviceProvider
            , IHangfireService hangfireService
            , ILoggerFactory loggerFactory)
        {
            ApplicationDependencyResolver.Init(app.ApplicationServices);

            if (environment.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }


            //app.UseMiddleware(typeof(ErrorHandlingMiddleware));
            //app.UseMvc();
            app.UseAuthentication();

            app.UseHttpsRedirection();

            //for identity server
            app.UseIdentityServer();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            //app.UseSession();

            app.UseCors("CorsPolicy");
            app.UseCors("AllowHeaders");


            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.EnableFilter();
                c.InjectStylesheet("./../css/swagger-ui-themes/3.x/theme-flattop.css");
                c.ShowExtensions();
            });

            app.UseMiddleware(typeof(ExceptionMiddleware));

            app.UseMvcWithDefaultRoute();

            //GlobalConfiguration.Configuration.UseActivator(new HangfireActivator(serviceProvider));
            //app.UseHangfireServer();
            //app.UseHangfireDashboard();
            //app.UseHangfireDashboard("/hangfire", new DashboardOptions
            //{
            //    Authorization = new[] { new HangfireAuthorizationFilter() }
            //});

            app.ExecuteJob(hangfireService);
        }
    }
}
