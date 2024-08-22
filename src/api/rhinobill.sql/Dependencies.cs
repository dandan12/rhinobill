using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using rhinobill.sql.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace rhinobill.sql
{
    public static class Dependencies
    {
        public static IServiceCollection AddSql(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseInMemoryDatabase("rhinobill");
                //options.UseInMemoryDatabase("rhinobill");
            });


            services
                .AddAutoMapper(typeof(StudenProfile).Assembly)
                .Scan(scan => scan
                .FromAssemblyOf<AppDbContext>()
                .AddClasses()
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            return services;
        }
    }
}
