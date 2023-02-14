using CudJobUI.Contracts;
using CudJobUI.Data;
//using CudJobUI.Models;
using CudJobUI.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SmartBreadcrumbs.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;
using Microsoft.AspNetCore.Authentication.Cookies;
using CudJobUI.ViewModels;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Http;

namespace CudJobUI
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
            services.AddControllersWithViews(
                optios =>
                {
                    optios.Filters.Add<SessionTimeoutAttribute>();
                }
                );
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IStaticEndPoints, StaticEndPoints>();
            services.AddScoped<IFileoperations, Fileoperations>();
            services.AddScoped<ICustomFunctions, CustomFunctions>();

            services.AddDbContextPool<CUDJobDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DataConnection"));
            });

            services.AddDistributedMemoryCache();

            var cookiesConfig = this.Configuration.GetSection("cookies")
            .Get<CookiesConfig>();
            services.AddAuthentication(
                CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.Cookie.Name = cookiesConfig.CookieName;
                    options.LoginPath = cookiesConfig.LoginPath;
                    options.LogoutPath = cookiesConfig.LogoutPath;
                    options.AccessDeniedPath = cookiesConfig.AccessDeniedPath;
                    options.ReturnUrlParameter = cookiesConfig.ReturnUrlParameter;
                    //options.ExpireTimeSpan = TimeSpan.FromSeconds(30);
                });

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromHours(2);
               // options.IdleTimeout = TimeSpan.FromSeconds(30);
            });


           // services.AddBreadcrumbs(GetType().Assembly);
            //services.AddBreadcrumbs(GetType().Assembly, options =>
            //{
            //    options.TagName = "nav";
            //    options.TagClasses = "";
            //    options.OlClasses = "breadcrumb";
            //    options.LiClasses = "breadcrumb-item";
            //    options.ActiveLiClasses = "breadcrumb-item active";
            //    options.SeparatorElement = "<li class=\"separator\">/</li>";
            //});
            //services.AddControllersWithViews(Options =>
            //{
            //    Options.Filters.Add(typeof(SessionTimeoutAttribute));
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseSession();

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=Login}/{id?}");
            });
        }
    }
}
