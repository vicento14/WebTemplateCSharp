using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebTemplateCSharp
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddHttpContextAccessor();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseRouting();
            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                /*endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });*/
                endpoints.MapDefaultControllerRoute();
            });

        }
    }

    public class RedirectFilterLoginAdminAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var username = context.HttpContext.Session.GetString("_username");
            var role = context.HttpContext.Session.GetString("_role");

            if (string.IsNullOrEmpty(username))
            {
                context.Result = new RedirectToActionResult("Index", "Login", null);
            }
            else if (role == "user")
            {
                context.Result = new RedirectToActionResult("Pagination", "User", null);
            }
        }
    }

    public class RedirectFilterLoginUserAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var username = context.HttpContext.Session.GetString("_username");
            var role = context.HttpContext.Session.GetString("_role");

            if (string.IsNullOrEmpty(username))
            {
                context.Result = new RedirectToActionResult("Index", "Login", null);
            }
            else if (role == "admin")
            {
                context.Result = new RedirectToActionResult("Dashboard", "Admin", null);
            }
        }
    }

    public class RedirectFilterAdminUserAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var username = context.HttpContext.Session.GetString("_username");
            var role = context.HttpContext.Session.GetString("_role");

            if (!string.IsNullOrEmpty(username))
            {
                if (role == "admin")
                {
                    context.Result = new RedirectToActionResult("Dashboard", "Admin", null);
                }
                else if (role == "user")
                {
                    context.Result = new RedirectToActionResult("Pagination", "User", null);
                }
            }
        }
    }
    
}
