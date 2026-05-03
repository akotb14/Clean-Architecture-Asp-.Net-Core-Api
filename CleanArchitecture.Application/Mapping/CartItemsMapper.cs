using AutoMapper;
using CleanArchitecture.Application.Features.ShoppingCarts.Commads.Requests;
using CleanArchitecture.Application.Features.ShoppingCarts.Queries.Responses;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Helper.Dtos;

namespace CleanArchitecture.Application.Mapping
{
    public class CartItemsMapper : Profile
    {
        public CartItemsMapper()
        {
            CreateMap<AddCartItemsCommand, CartItem>();
            CreateMap<CartItem, GetCartandItemsResponse>()
                .ForMember(e => e.ProductName, s => s.MapFrom(op => op.Product.Name))
                .ForMember(e => e.ImageURL, s => s.MapFrom(op => op.Product.ImageURL))
                .ForMember(e => e.ProductDescription, s => s.MapFrom(op => op.Product.Description));

            CreateMap<CartItem, OrderItemsDto>()
               .ForMember(d => d.name, s => s.MapFrom(e => e.Product.Name))
               .ForMember(d => d.amount_cents, s => s.MapFrom(e => e.UnitPrice))
               .ForMember(d => d.description, s => s.MapFrom(e => e.Product.Description))
               .ForMember(d => d.quantity, s => s.MapFrom(e => e.Quantity));
        }
    }
}
