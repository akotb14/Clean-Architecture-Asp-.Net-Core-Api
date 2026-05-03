namespace CleanArchitecture.Application.ResultHandler
{
    public class PaymobAuthResponse
    {
        public string Token { get; set; }
    }
    public class PaymobOrderResponse
    {
        public int Id { get; set; }
        public string PaymentKey { get; set; }
    }
    public class PaymobPaymentKeyResponse
    {
        public string Token { get; set; }
    }
}
