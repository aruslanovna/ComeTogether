using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

using ComeTogether.Service.Interfaces;
using ComeTogether.Service.Services;

namespace ComeTogether.Service
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
           
            services.AddTransient<IProjectService, ProjectService>();
            services.AddTransient<IArticleService, ArticleService>();
          //  services.AddTransient<ICategoryService, CategoryService>();

            services.AddScoped<IEmailSender, EmailSender>();

            return services;
        }
    }
}
