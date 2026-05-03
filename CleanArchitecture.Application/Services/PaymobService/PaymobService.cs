using CleanArchitecture.Application.Features.Orders.Commands.Requests;
using CleanArchitecture.Application.ResultHandler;
using CleanArchitecture.Domain.Helper.Dtos;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;

namespace CleanArchitecture.Application.Services.PaymobService
{
    public class PaymobService : IPaymobService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public PaymobService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<string> AuthenticateAsync()
        {
            var apiKey = _configuration["Paymob:ApiKey"];
            var requestBody = new { api_key = apiKey };

            var response = await _httpClient.PostAsJsonAsync("https://accept.paymob.com/api/auth/tokens", requestBody);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<PaymobAuthResponse>();
            return result.Token;
        }

        public async Task<PaymobOrderResponse> CreateOrderAsync(string token, decimal amount, List<OrderItemsDto> items)
        {
            var requestBody = new
            {
                auth_token = token,
                delivery_needed = true,
                amount_cents = (int)(amount * 100),
                currency = "EGP",
                items = items
            };

            var response = await _httpClient.PostAsJsonAsync("https://accept.paymob.com/api/ecommerce/orders", requestBody);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<PaymobOrderResponse>();
        }



        public async Task<string> GeneratePaymentKeyAsync(string token, int orderId, decimal amount, CreateOrderCommand request)
        {
            var integrationId = _configuration["Paymob:IntegrationId"];

            var billingData = new BillingDataDto()
            {
                first_name = request.FirstName,
                last_name = request.LastName,
                phone_number = request.PhoneNumber,
                email = request.Email,
                country = request.Country,
                street = request.Street,
                state = request.State,
                building = request.Building,
                postal_code = request.PostalCode,
                floor = request.Floor,
                apartment = request.Apartment,
                city = request.City
            };

            var requestBody = new
            {
                auth_token = token,
                amount_cents = (int)(amount * 100),
                currency = "EGP",
                order_id = orderId,
                expiration = 3600,
                billing_data = billingData,
                integration_id = integrationId
            };
            var response = await _httpClient.PostAsJsonAsync("https://accept.paymob.com/api/acceptance/payment_keys", requestBody);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<PaymobPaymentKeyResponse>();
            return result.Token;
        }
    }
}
