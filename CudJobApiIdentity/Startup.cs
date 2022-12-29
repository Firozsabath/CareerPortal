using AutoMapper;
using CUDJobApiIdentity.Contracts;
using CUDJobApiIdentity.Data;
using CUDJobApiIdentity.Services;
using CUDJobAPiIdentity.Contracts;
using CUDJobAPiIdentity.Data;
using CUDJobAPiIdentity.Ldap;
using CUDJobAPiIdentity.Mappings;
using CUDJobAPiIdentity.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CudJobApiIdentity
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
            services.AddDatabaseDeveloperPageExceptionFilter();
            //services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)

            services.AddDefaultIdentity<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(o =>
                {
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["JWT:ValidIssuer"],
                        ValidAudience = Configuration["JWT:ValidAudience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]))
                    };
                });

            services.AddCors(o => {
                o.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });

            services.AddSingleton<ILoggerService, LoggerService>();

            //services.AddControllers()
            //         .AddNewtonsoftJson(options =>
            //         {
            //             options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            //         });
            services.AddControllers()
                .AddNewtonsoftJson(options =>
                     options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddAutoMapper(typeof(Maps));            

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CUDJobAPiIdentity", Version = "v1" });
            });      
    

            services.Configure<LdapConfig>(this.Configuration.GetSection("ldap"));
            services.AddScoped<IAuthenticationService, LdapAuthenticationService>();
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<ISupportFunction, SupportFunction>();
            services.AddScoped<IJobRepository, JobRepository>();
            services.AddScoped<IEmailConfig, EmailConfig>();
            services.AddScoped<IjobApplicationRespository, jobApplicationRepository>();
            services.AddScoped<IStudentPersnlRepository, StudentPersnlRepository>();
            services.AddScoped<INotesRepository, NotesRepository>();
            services.AddScoped<IExternalFunctions, ExternalFunctions>();

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
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Require.Ldap.User", policy =>
                                  policy.RequireClaim("aspnetcore.ldap.user", "true")
                                        .AddAuthenticationSchemes(CookieAuthenticationDefaults.AuthenticationScheme)
                                      );
            });
         
            //services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, 
            IWebHostEnvironment env,
            UserManager<IdentityUser> usermanager,
            RoleManager<IdentityRole> rolemanager
            )
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CUDJobAPiIdentity v1"));
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }           

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseHttpsRedirection();

            app.UseCors("CorsPolicy");

            SeedData.Seed(usermanager, rolemanager).Wait();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
