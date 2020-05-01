
using ComeTogether.Infrastructure.Interface;
using ComeTogether.Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Security.Claims;

namespace ComeTogether.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
           
            services.AddScoped<IUnitOfWork, UnitOfWork>();
          

            return services;
        }
    }
}
