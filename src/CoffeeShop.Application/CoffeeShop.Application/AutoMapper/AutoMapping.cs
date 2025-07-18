using AutoMapper;
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
    .ForMember(dest => dest.PeiIntValorUnit, opt => opt.MapFrom(src => src.PrecoUnitario))
    .ForMember(dest => dest.PeiIntValorTotal, opt => opt.Ignore())
    .ForMember(dest => dest.PeiIdPedido, opt => opt.Ignore()) // será preenchido fora do AutoMapper
    .ForMember(dest => dest.PeiIdPedidoNavigation, opt => opt.Ignore())
    .ForMember(dest => dest.PeiIdPrecoNavigation, opt => opt.Ignore())
    .ForMember(dest => dest.PeiIdProdutoNavigation, opt => opt.Ignore());

            //CreateMap<RequestRegisterUserJson, User>().
            //    ForMember(dest => dest.Password, config => config.Ignore());
        }

        private void EntityToResponse()
        {
            //CreateMap<Expense, ResponseRegisterExpenseJson>();
            //CreateMap<Expense, ResponseShortExpenseJson>();
            //CreateMap<Expense, ResponseExpenseJson>();
        }

    }
}
