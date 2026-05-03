using AutoMapper;
using CleanArchitecture.Application.Features.Orders.Commands.Requests;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Mapping
{
    public class ShippingMethodsMapper : Profile
    {
        public ShippingMethodsMapper()
        {
            CreateMap<CreateOrderCommand, ShippingMethod>();
        }
    }
}
