using ComeTogether.Infrastructure.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

using WebMVC.IdentityPrivacy;


using WebMVC.SignalR;
using ComeTogether.Infrastructure;


using ComeTogether.Service;
using ComeTogether.Domain.MessageModel;
using Microsoft.OpenApi.Models;
using ComeTogether.Domain.Entities;
using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.IO;
using Microsoft.AspNetCore.Identity.UI.Services;
using WebMVC.Areas.Identity.Pages.Account;
using ComeTogether.Service.Services;
using Microsoft.Extensions.Logging;

namespace WebMVC
{
    public class Startup
    {
        private readonly OpenApiInfo openApiInfo = new OpenApiInfo
        {
            Title = "Sense HAT API",
            Version = "v1"
        };
        public Startup(IConfiguration configuration )
        {
             //manager= _manaoer;
             Configuration = configuration;
        }
        public UserManager<ApplicationUser> manager { get; }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddSignalR();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            var emailConfig = Configuration
      .GetSection("EmailConfiguration")
      .Get<EmailConfiguration>();
            services.AddSingleton(emailConfig);
            services.AddTransient<IPasswordValidator<ApplicationUser>, CustomPasswordPolicy>();
            services.AddTransient<IUserValidator<ApplicationUser>, CustomUsernameEmailPolicy>();
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:ApplicationConnection"]));
            services.AddTransient<IAuthorizationHandler, AllowUsersHandler>();
            services.AddTransient<IAuthorizationHandler, AllowPrivateHandler>();
          
          
            services.AddInfrastructure();
            services.AddServices();
            // services.AddScoped<IPollManager, PollManager>();
            services.AddAuthorization(opts =>
            {
                opts.AddPolicy("AspManager", policy =>
                {
                    policy.RequireRole("Manager");
                    policy.RequireClaim("Coding-Skill", "ASP.NET Core MVC");
                });
            });
            services.AddAuthorization(opts =>
            {
                opts.AddPolicy("AllowTom", policy =>
                {
                    policy.AddRequirements(new AllowUserPolicy("tom"));
                });
            });
            services.AddAuthorization(opts =>
            {
                opts.AddPolicy("PrivateAccess", policy =>
                {
                    policy.AddRequirements(new AllowPrivatePolicy());
                });
            });
            services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null)
              .AddNewtonsoftJson();

            // Swagger generator
            services.AddSwaggerGen(o =>
            {
                o.SwaggerDoc(openApiInfo.Version, openApiInfo);
            });

            // SenseHatService (emulation mode is obtained from appsettings.json)
           
            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            //{
            //    options.RequireHttpsMetadata = false;
            //    options.SaveToken = true;
            //    options.TokenValidationParameters = new TokenValidationParameters()
            //    {
            //        ValidateIssuer = true,
            //        ValidateAudience = true,
            //        ValidAudience = Configuration["Jwt:Audience"],
            //        ValidIssuer = Configuration["Jwt:Issuer"],
            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
            //    };
            //});

            services.AddIdentity<ApplicationUser, IdentityRole>(opts =>
            {
                opts.SignIn.RequireConfirmedAccount = false;
                opts.User.RequireUniqueEmail = true;
               // opts.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyz@0-9";
                opts.Password.RequiredLength = 8;
                opts.Password.RequireNonAlphanumeric = true;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireUppercase = true;
                opts.Password.RequireDigit = true;
            }).AddEntityFrameworkStores<ApplicationDbContext>()/*.AddDefaultTokenProviders()*/;
            services.AddMvc();
            services.AddScoped<UserManager<ApplicationUser>, UserManager<ApplicationUser>>();
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(
                Configuration.GetConnectionString("ApplicationConnection")));
         //   services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
         //.AddEntityFrameworkStores<ApplicationDbContext>();
            //services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddSingleton<IUserIdProvider, CustomUserIdProvider>();
           
               //.AddDataAnnotationsLocalization()
               // .AddViewLocalization().AddRazorOptions(
               //  options => {
               //  options.ViewLocationFormats.Add("/Account/Login.cshtml");
               //  });

            services.AddLocalization(options => options.ResourcesPath = "LanguageResources");
            services.AddControllersWithViews()
                  .AddDataAnnotationsLocalization()
                 .AddViewLocalization();
            services.ConfigureApplicationCookie(opts => opts.LoginPath = "/Identity/Account/Login");
            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                    new CultureInfo("en"),
                    new CultureInfo("uk")

                };

                options.DefaultRequestCulture = new RequestCulture("en");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });
            services.AddRazorPages();
           
            services.ConfigureApplicationCookie(options => {              
                options.Cookie.HttpOnly = true;              
                options.LoginPath = "/Identity/Account/Login";            
                options.AccessDeniedPath = "/AccessDenied";
                options.SlidingExpiration = true;

            });

        }
    
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {

            

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseRequestLocalization();

            app.UseHttpsRedirection();
        

            app.UseRouting();

            app.UseStatusCodePages(async context =>
            {
                var response = context.HttpContext.Response;

                if (response.StatusCode == (int)HttpStatusCode.Unauthorized ||
                    response.StatusCode == (int)HttpStatusCode.Forbidden)
                    response.Redirect("/Identity/Account/Login");
            });
            app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
             app.UseCookiePolicy();

            app.UseRouting();
            // app.UseRequestLocalization();
             app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
               
                endpoints.MapHub<ChatHub>("/chat");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
            app.Run(async (context) => loggerFactory.AddFile(Path.Combine(Directory.GetCurrentDirectory(), "logger.txt")));
            var logger = loggerFactory.CreateLogger("FileLogger");

            app.Run(async (context) =>
            {
                logger.LogInformation("Processing request {0}", context.Request.Path);
                await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}
