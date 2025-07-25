using Amazon;
using Amazon.CognitoIdentityProvider;
using Amazon.Runtime;
using CoffeeShop.Application.ExternalServices.Contracts.AWS;
using CoffeeShop.Application.ExternalServices.Contracts.Stripe;
using CoffeeShop.Application.ExternalServices.DTO.AWS;
using CoffeeShop.Domain;
using CoffeeShop.Domain.Repositories.Especificas;
using CoffeeShop.Domain.Repositories.LocalRepository;
using CoffeeShop.Infraestructure.DataAccess;
using CoffeeShop.Infraestructure.DataAccess.Repositories.Especificos;
using CoffeeShop.Infraestructure.DataAccess.Repositories.LocalRepository;
using CoffeeShop.Infraestructure.ExternalServices.AWS.EmailService;
using CoffeeShop.Infraestructure.ExternalServices.AWS.JWT;
using CoffeeShop.Infraestructure.ExternalServices.Stripe.CreateSession;
using CoffeeShop.Infraestructure.ExternalServices.Stripe.ExpireCheckoutSession;
using CoffeeShop.Infraestructure.ExternalServices.Stripe.GetSession;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

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
           .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information)
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
            AddLocalRepositoryConfig(services,configuration);

            var awsSettings = configuration.GetSection("AWS").Get<AwsEmailSettings>()!;
            var awsOptions = Options.Create(awsSettings);

            var options = Options.Create(awsSettings);

            services.AddScoped<ISesEmailService>(sp =>
                new SesEmailService(awsOptions));

            services.AddScoped<IJwtTokenService>(sp =>
            {
                var creds = new BasicAWSCredentials(awsSettings.AccessKey, awsSettings.SecretKey);
                var region = RegionEndpoint.GetBySystemName(awsSettings.Region);
                var client = new AmazonCognitoIdentityProviderClient(creds, region);

                return new AuthService(client, awsSettings.CognitoClientId!, awsSettings.ClientSecret, awsSettings.CognitoUserPoolId!);
            });
            services.AddScoped<ICreateUserAuth>(sp =>
            {
                var creds = new BasicAWSCredentials(awsSettings.AccessKey, awsSettings.SecretKey);
                var region = RegionEndpoint.GetBySystemName(awsSettings.Region);
                var client = new AmazonCognitoIdentityProviderClient(creds, region);

                return new CreateUserAuth(client, awsSettings.CognitoUserPoolId!);
            });
            services.AddScoped<ICreateCheckoutSession, CreateCheckoutSession>();
            services.AddScoped<IGetSessionStatus, GetSessionStatus>();
            services.AddScoped<IExpireCheckoutSession, ExpireCheckoutSession>();
        }


        private static void AddLocalRepositoryConfig(IServiceCollection services, IConfiguration configuration)
        {

            var redisConnection = configuration.GetConnectionString("Redis")!;
            var multiplexer = ConnectionMultiplexer.Connect(redisConnection);
            services.AddSingleton<IConnectionMultiplexer>(multiplexer);
            services.AddScoped<ICacheService, RedisCacheService>();
        }
    }
}
