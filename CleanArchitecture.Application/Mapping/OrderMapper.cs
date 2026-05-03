using AutoMapper;
using CleanArchitecture.Application.Features.Orders.Queries.Responses;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Mapping
{
    public class OrderMapper : Profile
    {
        /* orderID OrderDate TotalAmount Status Order.Quantity Order.Size Product.Name Product.Description Product.ImageUrl  */
        public OrderMapper()
        {
            CreateMap<CartItem, OrderItem>();
            CreateMap<OrderItem, GetOrdersResponse>()
                .ForMember(s => s.ProductName, e => e.MapFrom(d => d.Product.Name))
                .ForMember(s => s.ProductDescription, e => e.MapFrom(d => d.Product.Description))
                .ForMember(s => s.ImageUrl, e => e.MapFrom(d => d.Product.ImageURL))
                .ForMember(s => s.Quantity, e => e.MapFrom(d => d.Quantity))
                .ForMember(s => s.Status, e => e.MapFrom(d => d.Order.Status))
                .ForMember(s => s.Size, e => e.MapFrom(d => d.Size))
                .ForMember(s => s.OrderDate, e => e.MapFrom(d => d.Order.OrderDate))
                .ForMember(s => s.OrderID, e => e.MapFrom(d => d.OrderID))
                .ForMember(s => s.TotalPrice, e => e.MapFrom(d => d.TotalPrice));

        }
    }
}
