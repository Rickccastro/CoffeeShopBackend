using CoffeeShop.Communication.Requests.Customer;
using CoffeeShop.Communication.Responses;


namespace CoffeeShop.Application.UseCase.Customer.Create
{
    public interface ICreateCustomerUseCase 
    {
        CustomerResponse CreateCustomer(CustomerRequest request);
    }
}
