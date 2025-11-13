using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;
using Serilog;
using Microsoft.Extensions.DependencyInjection;
using IdentityModel;
using System.Security.Claims;

using Microsoft.AspNetCore.Identity;
using System.Linq;
using Softcode.GTex.Data;
using Softcode.GTex.Data.Models;

namespace Softcode.GTex.Web.Api
{
    public class Program
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                 .ReadFrom.Configuration(ApplicationConfigurations.Configuration)
                 .Enrich.FromLogContext()
                 .CreateLogger();

            try
            {
                Log.Information("Getting the motors running...");

                IWebHost host = BuildWebHost(args);

                //SeedData(host);

                host.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
               .UseKestrel()
                .UseConfiguration(ApplicationConfigurations.Configuration)
                .ConfigureServices(services => services.AddAutofac())
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .UseSerilog()
                .SuppressStatusMessages(true)
                .UseUrls(ApplicationConfigurations.ApplicationUrl)
                .Build();

        private static void SeedData(IWebHost host)
        {
            using (var scope = host.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ApplicationDbContext>();

                var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var alice = userMgr.FindByNameAsync("admin").Result;
                if (alice == null)
                {
                    alice = new ApplicationUser
                    {
                        UserName = "admin",
                        ContactId = 1,
                        CreatedByContactId = 1,
                        CreatedDateTime = DateTime.Now
                    };
                    var result = userMgr.CreateAsync(alice, "Admin123#").Result;
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }

                    //result = userMgr.AddClaimsAsync(alice, new Claim[]{
                    //        new Claim(JwtClaimTypes.Name, "System Administrator"),
                    //        new Claim(JwtClaimTypes.GivenName, "System Administrator"),
                    //        new Claim(JwtClaimTypes.FamilyName, "Administrator"),
                    //        new Claim(JwtClaimTypes.Email, "info@soft-code.net"),
                    //        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                    //        new Claim(JwtClaimTypes.WebSite, "http://soft-code.net"),
                    //        new Claim(JwtClaimTypes.Address, @"{ 'street_address': 'One Hacker Way', 'locality': 'Heidelberg', 'postal_code': 69118, 'country': 'Germany' }", IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json)
                    //    }).Result;

                    //if (!result.Succeeded)
                    //{
                    //    throw new Exception(result.Errors.First().Description);
                    //}
                    Console.WriteLine("alice created");
                }
                else
                {
                    Console.WriteLine("alice already exists");
                }

            }
        }
    }
}
