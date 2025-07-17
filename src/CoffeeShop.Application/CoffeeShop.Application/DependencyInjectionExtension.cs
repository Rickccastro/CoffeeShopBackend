using CoffeeShop.Application.UseCase.Checkout.Create;
using CoffeeShop.Application.UseCase.Checkout.GetSessionStatus;
using CoffeeShop.Application.UseCase.Customer.Create;
using CoffeeShop.Application.UseCase.Customer.Login;
using CoffeeShop.Application.UseCase.Email.EmailServiceNotification;
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
            //services.AddAutoMapper(typeof(AutoMapping));
        }

        public static void AddUseCase(IServiceCollection services)
        {
            services.AddScoped<ICreateCheckoutUseCase, CreateCheckoutUseCase>();
            services.AddScoped<ICreateUserUseCase, CreateUserUseCase>();
            services.AddScoped<ILoginUserUseCase,  LoginUserUseCase>();
            services.AddScoped<IEmailSenderNotification, EmailSenderNotificationUseCase>();
            services.AddScoped<IGetSessionStatusUseCase, GetSessionStatusUseCase>();
            //services.AddScoped<IGetAllExpenseUseCase, GetAllExpenseUseCase>();
            //services.AddScoped<IGetExpenseByIdUseCase, GetExpenseByIdUseCase>();
            //services.AddScoped<IUpdateExpenseUseCase, UpdateExpenseUseCase>();
            //services.AddScoped<IDeleteExpenseUseCase, DeleteExpenseUseCase>();
            //services.AddScoped<IGenerateExpensesReportExcelUseCase, GenerateExpensesReportExcelUseCase>();
            //services.AddScoped<IGenerateExpensesReportPdfUseCase, GenerateExpensesReportPdfUseCase>();
            //services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
            //services.AddScoped<IDoLoginUseCase, DoLoginUseCase>();
        }
    }
}
