using CoffeeShop.Domain.Repositories;
using CoffeeShop.Infraestructure.DataAccess.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop.Infraestructure
{
    public static class DependencyInjectionExtension
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddScoped<IPasswordEncripter, Security.Cryptography.BCrypt>();
            //services.AddScoped<ILoggedUser, LoggedUser>();

            //AddToken(services, configuration);
            AddRepositories(services);

            //if (configuration.IsTestEnvironment() == false)
            //{
            //    AddDbContext(services, configuration);
            //}
        }

        private static void AddRepositories(IServiceCollection services)
        {
            //services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IPrecoRepository, PrecoRepository>();
        }
    }
}
