using AutoMapper;
using CoffeeShop.Communication.Requests.User;
using CoffeeShop.Communication.Responses;
using CoffeeShop.Domain.Entities;
using CoffeeShop.Domain.ExternalServices.Stripe.Entities;
namespace CoffeeShop.Application.AutoMapper
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            RequestToEntity();
            EntityToResponse();
        }

        private void RequestToEntity()
        {
            CreateMap<CheckoutItemRequest, PeiPedidoIten>()
    .ForMember(dest => dest.PeiIdProduto, opt => opt.MapFrom(src => src.ProdutoId))
    .ForMember(dest => dest.PeiIdPreco, opt => opt.MapFrom(src => src.PrecoId))
    .ForMember(dest => dest.PeiIntQuantidade, opt => opt.MapFrom(src => src.Quantidade))
    .ForMember(dest => dest.PeiIntValorUnit, opt => opt.MapFrom(src => src.PrecoUnitario));


            CreateMap<UserRequest, UsrUsuario>()
              .ForMember(dest => dest.UsrEmail, opt => opt.MapFrom(src => new EsnEmailServicoNotificacao
              {
                  EmailNm = src.EmailNm
              }))
              .AfterMap((src, dest) =>
              {
                  dest.UsrId = Guid.NewGuid(); 
                  dest.UsrEmail.EmailId = Guid.NewGuid();
                  dest.UsrEmail.EmailUsrId = dest.UsrId;
              });
        }

        private void EntityToResponse()
        {
            CreateMap<UsrUsuario, UserResponse>();
        }
    }
}
