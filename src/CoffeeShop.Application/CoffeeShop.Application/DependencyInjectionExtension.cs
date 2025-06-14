using CoffeeShop.Application.UseCase.Checkout.Create;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
