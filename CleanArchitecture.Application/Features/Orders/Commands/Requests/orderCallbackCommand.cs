using CleanArchitecture.Application.ResultHandler;
using MediatR;

namespace CleanArchitecture.Application.Features.Orders.Commands.Requests
{
    public class OrderCallbackCommand : IRequest<Response<string>>
    {
        public string type { get; set; }
        public TransactionObj obj { get; set; }

        public class TransactionObj
        {
            public int id { get; set; }
            public bool pending { get; set; }
            public int amount_cents { get; set; }
            public bool success { get; set; }
            public bool is_auth { get; set; }
            public bool is_capture { get; set; }
            public bool is_voided { get; set; }
            public bool is_refunded { get; set; }
            public bool is_3DSecure { get; set; }
            public int integration_id { get; set; }

            public Order order { get; set; }
            public string currency { get; set; }

        }

        public class Order
        {
            public int id { get; set; }
            public DateTime created_at { get; set; }
            public bool delivery_needed { get; set; }
            public int amount_cents { get; set; }
            public ShippingData shipping_data { get; set; }
            public string currency { get; set; }

            public string? merchant_order_id { get; set; }
            public object? wallet_notification { get; set; }
            public int paid_amount_cents { get; set; }

        }


        public class ShippingData
        {
            public int Id { get; set; }
            public string First_name { get; set; }
            public string Last_name { get; set; }
            public string Street { get; set; }
            public string Building { get; set; }
            public string Floor { get; set; }
            public string Apartment { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string Country { get; set; }
            public string Email { get; set; }
            public string phone_number { get; set; }
            public string postal_code { get; set; }
            public string extra_description { get; set; }
            public string Shipping_method { get; set; }
            public int Order_id { get; set; }
        }



    }

}
