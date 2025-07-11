using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Interfaces.UnitOfWorks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Suwen.Infrastructure.Repositories;
using Suwen.Persistence.Repositories;
using Suwen.Persistence.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suwen.Persistence
{
    public static class DependencyInjection
    {
        public static  IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));
            services.AddScoped(typeof(IWriteRepository<>), typeof(WriteRepository<>));
           
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
