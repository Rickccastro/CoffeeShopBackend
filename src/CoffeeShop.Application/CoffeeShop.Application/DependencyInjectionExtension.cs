using CoffeeShop.Application.AutoMapper;
using CoffeeShop.Application.UseCase.Checkout.Create;
using CoffeeShop.Application.UseCase.Checkout.GetSessionStatus;
using CoffeeShop.Application.UseCase.Customer.Create;
using CoffeeShop.Application.UseCase.Customer.Login;
using CoffeeShop.Application.UseCase.Email.EmailServiceNotification;
using CoffeeShop.Application.UseCase.Pedido.GetTotalValorPedido;
using CoffeeShop.Application.UseCase.PedidoItem.CreatePedidoItem;
using CoffeeShop.Application.UseCase.Preco.GetPrecoVigente;
using CoffeeShop.Application.UseCase.Preco.GetPrecoVIgente;
using Microsoft.Extensions.DependencyInjection;

namespace CoffeeShop.Application
{
    public static class DependencyInjectionExtension
    {
        public static void AddApplication(this IServiceCollection services)
        {
            AddUseCase(services);
            AddAutoMapping(services);
        }

        public static void AddAutoMapping(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapping));
        }

        public static void AddUseCase(IServiceCollection services)
        {
            services.AddScoped<ICreateCheckoutUseCase, CreateCheckoutUseCase>();
            services.AddScoped<ICreateUserUseCase, CreateUserUseCase>();
            services.AddScoped<ILoginUserUseCase,  LoginUserUseCase>();
            services.AddScoped<IEmailSenderNotification, EmailSenderNotificationUseCase>();
            services.AddScoped<IGetSessionStatusUseCase, GetSessionStatusUseCase>();
            services.AddScoped<ICreatePedidoItemUseCase, CreatePedidoItemUseCase>();
            services.AddScoped<IGetPrecoVigenteUseCase, GetPrecoVigenteUseCase>();
            services.AddScoped<IGetTotalValorPedidoUseCase, GetTotalValorPedidoUseCase>();
        }
    }
}
