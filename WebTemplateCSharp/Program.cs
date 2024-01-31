using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTemplateCSharp.Entities;

namespace WebTemplateCSharp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureServices((hContext, services) =>
                {
                    var config = hContext.Configuration;
                    const string DB_CONTEXT_CONNSTRING = "MySqlConnection1";
                    services.AddDbContext<UserAccountsDbContext>(options =>
                    {
                        options.UseMySQL(config.GetConnectionString(DB_CONTEXT_CONNSTRING));
                    });
                    services.AddDbContext<TT1DbContext>(options =>
                    {
                        options.UseMySQL(config.GetConnectionString(DB_CONTEXT_CONNSTRING));
                    });
                    services.AddDbContext<TT2DbContext>(options =>
                    {
                        options.UseMySQL(config.GetConnectionString(DB_CONTEXT_CONNSTRING));
                    });
                    /*services.AddDbContext<UserAccountsDbContext>(options =>
                    {
                        options.UseMySQL(config.GetConnectionString(USERACCOUNTSDB_CONTEXT_CONNSTRING));
                    }, ServiceLifetime.Singleton);*/

                    services.AddDistributedMemoryCache();

                    services.AddSession(options =>
                    {
                        options.IdleTimeout = TimeSpan.FromMinutes(10);
                        options.Cookie.HttpOnly = true;
                        options.Cookie.IsEssential = true;
                    });

                    services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

                    /*services.AddControllers()
                        .ConfigureApiBehaviorOptions(options =>
                        {
                            options.SuppressModelStateInvalidFilter = true;
                        });*/
                });
    }
}
