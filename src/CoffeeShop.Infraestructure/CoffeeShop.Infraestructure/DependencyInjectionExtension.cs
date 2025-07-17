using CoffeeShop.Domain;
using CoffeeShop.Domain.ExternalServices.AWS.Email;
using CoffeeShop.Domain.ExternalServices.Stripe;
using CoffeeShop.Domain.Repositories.Especificas;
using CoffeeShop.Infraestructure.DataAccess;
using CoffeeShop.Infraestructure.DataAccess.Repositories.Especificos;
using CoffeeShop.Infraestructure.ExternalServices.AWS.EmailService;
using CoffeeShop.Infraestructure.ExternalServices.Stripe.CreateSession;
using CoffeeShop.Infraestructure.ExternalServices.Stripe.GetSession;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CoffeeShop.Infraestructure
{
    public static class DependencyInjectionExtension
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            AddRepositories(services);
            AddDbContext(services, configuration);
            AddExternalServices(services, configuration);
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
        private static void AddExternalServices(IServiceCollection services, IConfiguration configuration)
        {
            var awsSettings = new AwsEmailSettings
            {
                AccessKey = configuration["AWS:AccessKey"]!,
                SecretKey = configuration["AWS:SecretKey"]!,
                Region = configuration["AWS:Region"]!,
                FromAddress = configuration["AWS:FromAddress"]!,
                ConfigurationSet = configuration["AWS:ConfigurationSet"]!

            };

            var options = Options.Create(awsSettings);

            services.AddScoped<ISesEmailService>(sp => new SesEmailService(options));
            services.AddScoped<ICreateCheckoutSession, CreateCheckoutSession>();
            services.AddScoped<IGetSessionStatus, GetSessionStatus>();
        }
    }
}
