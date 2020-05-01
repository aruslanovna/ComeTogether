using ComeTogether.Infrastructure.Identity;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Globalization;

using WebMVC.IdentityPrivacy;

using WebMVC.Resources;
using WebMVC.SignalR;
using ComeTogether.Infrastructure;
using ApplicationUser = ComeTogether.Infrastructure.Identity.ApplicationUser;

using ComeTogether.Service;

namespace WebMVC
{
    public class Startup
    {
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

           // services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
            services.AddIdentity<ApplicationUser, IdentityRole>(opts =>
            {
                opts.SignIn.RequireConfirmedAccount = false;
                opts.User.RequireUniqueEmail = true;
                //opts.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyz";
                opts.Password.RequiredLength = 8;
                opts.Password.RequireNonAlphanumeric = true;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireUppercase = true;
                opts.Password.RequireDigit = true;
            }).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
            services.AddMvc();
            services.AddScoped<UserManager<ApplicationUser>, UserManager<ApplicationUser>>();
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(
                Configuration.GetConnectionString("DefaultConnection")));
         //   services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
         //.AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddSingleton<IUserIdProvider, CustomUserIdProvider>();
            services.AddControllersWithViews()
                .AddDataAnnotationsLocalization(options => {
                    options.DataAnnotationLocalizerProvider = (type, factory) =>
                        factory.Create(typeof(SharedResource));
                })
                .AddViewLocalization().AddRazorOptions(
                 options => {
                 options.ViewLocationFormats.Add("/Account/Login.cshtml");
                 });

         services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                    new CultureInfo("en"),
                    new CultureInfo("uk"),
                    new CultureInfo("ar")
                };

                options.DefaultRequestCulture = new RequestCulture("uk");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });
            services.AddRazorPages();

            services.ConfigureApplicationCookie(options => {
                // Cookie settings
                options.Cookie.HttpOnly = true;
               
                // If the LoginPath isn't set, ASP.NET Core defaults 
                // the path to /Account/Login.
                options.LoginPath = "/Identity/Account/Login";
                // If the AccessDeniedPath isn't set, ASP.NET Core defaults 
                // the path to /Account/AccessDenied.
                options.AccessDeniedPath = "/AccessDenied";
                options.SlidingExpiration = true;

            });

        }
    
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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
         
           // app.UseOwin();
            app.UseStatusCodePages();
            app.UseDeveloperExceptionPage();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            //app.UseSignalR2();
            //app.UseAuthentication();
            app.UseAuthorization();
       
            app.UseEndpoints(endpoints =>
            {
               
                endpoints.MapHub<ChatHub>("/chat");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
