using CoffeeShop.Communication.Requests.Customer;
using CoffeeShop.Communication.Responses;
using Stripe;



namespace CoffeeShop.Application.UseCase.Customer.Create
{
    public class CreateCustomerUseCase : ICreateCustomerUseCase
    {
        public CustomerResponse CreateCustomer(CustomerRequest request)
        {
            var options = new CustomerCreateOptions
            {
                Name = request.Nome,
                Email = request.Email,
                Metadata = new Dictionary<string, string>
                    {
                        { "cpf", request.Cpf },
                        { "senha", request.Senha }
                    }
            };

            var service = new CustomerService();
            service.Create(options);

            return new CustomerResponse
            {
                Nome = request.Nome,
                Cpf = request.Cpf,
                Email = request.Email,
                Senha = request.Senha   
            };
        }
    }
}
