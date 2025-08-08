using AutoMapper;
using CoffeeShop.Communication.Requests.User;
using CoffeeShop.Communication.Responses;
using CoffeeShop.Domain.Entities;
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
        //    CreateMap<CheckoutItemRequest, OriOrderItem>()
        //.ForMember(dest => dest.PeiIdPedidoItens, opt => opt.MapFrom(_ => Guid.NewGuid()))
        //.ForMember(dest => dest.PeiIdProduto, opt => opt.MapFrom(src => src.Produto.ProIdProduto))
        //.ForMember(dest => dest.PeiIdPreco, opt => opt.MapFrom(src => src.PrecoId))
        //.ForMember(dest => dest.PeiIntQuantidade, opt => opt.MapFrom(src => src.Quantidade))
        //.ForMember(dest => dest.PeiIntValorUnit, opt => opt.MapFrom(src => src.PrecoUnitario))
        //.ForMember(dest => dest.PeiIntValorTotal, opt => opt.MapFrom(src => src.ValorTotalItem));


            CreateMap<UserRequest, UsrUser>()
              .ForMember(dest => dest.SenServiceEmailNotifications, opt => opt.MapFrom(src => new SenServiceEmailNotification
              {
                  SenNmEmail = src.UsrEmailNm
              }))
              .AfterMap((src, dest) =>
              {
                  dest.UsrIdUser = Guid.NewGuid(); 
                  dest.SenServiceEmailNotifications.SenIdServiceEmailNotification = Guid.NewGuid();
                  dest.SenServiceEmailNotifications.SenUsrId = dest.UsrIdUser;
              });
        }

        private void EntityToResponse()
        {
            CreateMap<UsrUser, UserResponse>();
        }
    }
}
