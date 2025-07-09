using CoffeeShop.Domain;
using CoffeeShop.Domain.Repositories.Especificas;
using CoffeeShop.Infraestructure.DataAccess;
using CoffeeShop.Infraestructure.DataAccess.Repositories.Especificos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CoffeeShop.Infraestructure
{
    public static class DependencyInjectionExtension
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            AddRepositories(services);
            AddDbContext(services, configuration);
        }

        private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CoffeeShopDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("Connection"))
           .EnableSensitiveDataLogging()
           .EnableDetailedErrors()
           .LogTo(Console.WriteLine, LogLevel.Information)
            );
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IEmailServiceNotificationRepository, EmailServiceNotificationRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPedidoRepository, PedidoRepository>();
            services.AddScoped<IPedidoItensRepository, PedidoItensRepository>();
            services.AddScoped<IPrecoRepository, PrecoRepository>();
        }
    }
}
