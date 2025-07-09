using CoffeeShop.Application.UseCase.Checkout.Create;
using CoffeeShop.Application.UseCase.Customer.Create;
using CoffeeShop.Communication.Requests.Checkout;
using CoffeeShop.Communication.Requests.Customer;
using CoffeeShop.Communication.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShop.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        [Route("create-customer")]
        [HttpPost]
        public async Task <ActionResult> Create([FromServices] ICreateCustomerUseCase useCase, [FromBody] CustomerRequest request)
        {
            var resultCreateCheckoutUseCase =  await useCase.CreateCustomer(request);

            return Ok(resultCreateCheckoutUseCase);
        }


        [Route("login-customer")]
        [HttpPost]
        public ActionResult LoginCustomer([FromServices] ICreateCustomerUseCase useCase, [FromBody] CustomerRequest request)
        {
            var resultCreateCheckoutUseCase = useCase.CreateCustomer(request);

            return Ok(resultCreateCheckoutUseCase);
        }
    }
}
