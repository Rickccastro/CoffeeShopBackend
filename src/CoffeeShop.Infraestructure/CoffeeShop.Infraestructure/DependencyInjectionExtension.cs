using CoffeeShop.Application.Services.ExternalServices.Contracts.AWS;
using CoffeeShop.Application.Services.ExternalServices.Contracts.Stripe;
using CoffeeShop.Application.Services.ExternalServices.DTO.AWS;
using CoffeeShop.Application.Services.ExternalServices.DTO.Stripe;
using CoffeeShop.Application.Services.InternalServices;
using CoffeeShop.Application.Services.InternalServices.Security.Cryptography;
using CoffeeShop.Application.Services.InternalServices.Security.Token;
using CoffeeShop.Domain;
using CoffeeShop.Domain.Entities;
using CoffeeShop.Domain.Repositories.Especificas;
using CoffeeShop.Domain.Repositories.LocalRepository;
using CoffeeShop.Infraestructure.DataAccess;
using CoffeeShop.Infraestructure.DataAccess.Repositories.Especificos;
using CoffeeShop.Infraestructure.DataAccess.Repositories.LocalRepository;
using CoffeeShop.Infraestructure.Services.ExternalServices.AWS.EmailService;
using CoffeeShop.Infraestructure.Services.ExternalServices.Stripe.CreateCheckout;
using CoffeeShop.Infraestructure.Services.ExternalServices.Stripe.ExpireCheckoutSession;
using CoffeeShop.Infraestructure.Services.ExternalServices.Stripe.GetSession;
using CoffeeShop.Infraestructure.Services.ExternalServices.Stripe.WeebHook;
using CoffeeShop.Infraestructure.Services.InternalServices;
using CoffeeShop.Infraestructure.Services.InternalServices.Security.Cryptography;
using CoffeeShop.Infraestructure.Services.InternalServices.Security.Tokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
            AddInternalServices(services, configuration);
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
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IServiceEmailNotificationRepository, ServiceEmailNotificationRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderItemsRepository, OrderItemRepository>();
            services.AddScoped<IPriceRepository, PriceRepository>();
            services.AddScoped<IPaymentsRepository, PaymentRepository>();
        }

        private static void AddExternalServices(IServiceCollection services, IConfiguration configuration)
        {
            var stripeSettings = configuration.GetSection("Stripe").Get<StripeSettings>()!;
            services.AddSingleton(stripeSettings);

            // Registra StripeSettings como singleton para acesso direto (opcional)
            Stripe.StripeConfiguration.ApiKey = stripeSettings.SecretKey;


            AddLocalRepositoryConfig(services,configuration);

            var awsSettings = configuration.GetSection("AWS").Get<AwsEmailSettings>()!;
            var awsOptions = Options.Create(awsSettings);

            var options = Options.Create(awsSettings);

            services.AddScoped<ISesEmailService>(sp =>
                new SesEmailService(awsOptions));


            services.AddScoped<IGetSessionStatus, GetSessionStatus>();
            services.AddScoped<IWebHookHandler, WebHookHandler>();
            services.AddScoped<ICreateCheckoutSession, CreateCheckoutSession>();
            services.AddScoped<IExpireCheckoutSession, ExpireCheckoutSession>();
        }
        private static void AddInternalServices(IServiceCollection services, IConfiguration configuration)
        { 
            var expirationTimeMinutes = configuration.GetValue<uint>("Settings:Jwt:ExpiresMinutes");
            var signingKey = configuration.GetValue<string>("Settings:Jwt:SigningKey");

            var jwtSettings = configuration.GetSection("Settings:Jwt").Get<SettingsJwt>()!;
            services.AddSingleton(jwtSettings);

            services.AddScoped<IAccessTokenGenerator, JwtTokenGenerator>();
            services.AddScoped<IPasswordEncripter, BCryptor>();
            services.AddScoped<ILoggedUser, LoggedUser>();
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
