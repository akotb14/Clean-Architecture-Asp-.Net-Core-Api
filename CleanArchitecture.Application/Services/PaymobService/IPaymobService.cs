using CleanArchitecture.Application.Features.Orders.Commands.Requests;
using CleanArchitecture.Application.ResultHandler;
using CleanArchitecture.Domain.Helper.Dtos;

namespace CleanArchitecture.Application.Services.PaymobService
{
    public interface IPaymobService
    {

        Task<string> AuthenticateAsync();
        Task<PaymobOrderResponse> CreateOrderAsync(string token, decimal amount, List<OrderItemsDto> items);
        Task<string> GeneratePaymentKeyAsync(string token, int orderId, decimal amount, CreateOrderCommand request);

    }
}
