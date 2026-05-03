using AutoMapper;
using CleanArchitecture.Application.Features.Orders.Commands.Requests;
using CleanArchitecture.Application.ResultHandler;
using CleanArchitecture.Application.Services.CurrentUserService;
using CleanArchitecture.Application.Services.PaymobService;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Helper.Dtos;
using CleanArchitecture.Infrastructure.Repositories.CartItemsRepository;
using CleanArchitecture.Infrastructure.Repositories.CartRepository;
using CleanArchitecture.Infrastructure.Repositories.OrderItemsRepository;
using CleanArchitecture.Infrastructure.Repositories.OrderRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;
/*
 
https://accept.paymob.com/api/acceptance/iframes/867159?payment_token={payment_key_obtained_previously}
 4829524
 ZXlKaGJHY2lPaUpJVXpVeE1pSXNJblI1Y0NJNklrcFhWQ0o5LmV5SmpiR0Z6Y3lJNklrMWxjbU5vWVc1MElpd2ljSEp2Wm1sc1pWOXdheUk2T1RrME5EUXlMQ0p1WVcxbElqb2lNVGN5T0RJeU9EUTROQzQyTmpNd01qRWlmUS5uSm0ySnBPd2dqYnk2WW91MWkweHdZOWw2T2pGSXdVeTB4dDRsTWJ5T0FNcEZuSmZXU1llTFRPbDFHcFJOam1sc19zcjhabFEwcDU2el9BenpEQ0t6dw==
 */
namespace CleanArchitecture.Application.Features.Orders.Handlers
{
    public class OrderCommandHandler : ResponseHandler,
        IRequestHandler<CreateOrderCommand, Response<string>>,
        IRequestHandler<OrderCallbackCommand, Response<string>>
    {

        private readonly ICartRepository _cartRepository;
        private readonly ICartItemsRepository _cartItemsRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderItemsRepository _orderItemsRepository;
        private readonly ICurrentUserService _currentUser;
        private readonly IPaymobService _paymobService;
        private readonly IMapper _mapper;

        public OrderCommandHandler(IOrderRepository orderRepository, IOrderItemsRepository orderItemsRepository, IMapper mapper, ICurrentUserService currentUser, ICartRepository cartRepository, IPaymobService paymobService, ICartItemsRepository cartItemsRepository)
        {
            _orderRepository = orderRepository;
            _orderItemsRepository = orderItemsRepository;
            _mapper = mapper;
            _currentUser = currentUser;
            _cartRepository = cartRepository;
            _paymobService = paymobService;
            _cartItemsRepository = cartItemsRepository;
        }

        public async Task<Response<string>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var trans = await _cartRepository.BeginTransactionAsync();

                var user = _currentUser.GetUserId();

                var cart = await _cartRepository.GetTableNoTracking().FirstOrDefaultAsync(e => e.UserID == user);
                if (cart == null) { return NotFound<string>("cart not found"); }

                var cartItemsQueryable = _cartItemsRepository.GetTableNoTracking().Where(e => e.CartID == cart.CartID).AsQueryable();
                var cartItems = await cartItemsQueryable.ToListAsync();
                if (cartItems.Count() <= 0)
                {
                    return NotFound<string>("Not Found items");
                }
                var totalAmount = cartItems.Sum(e => e.TotalPrice);

                //paymob
                var data = await cartItemsQueryable.Include(e => e.Product).ToListAsync();
                var cartItemMapping = _mapper.Map<List<OrderItemsDto>>(data);

                string token = await _paymobService.AuthenticateAsync();
                var createOrderResponse = await _paymobService.CreateOrderAsync(token, totalAmount, cartItemMapping);
                var keyResponse = await _paymobService.GeneratePaymentKeyAsync(token, createOrderResponse.Id, totalAmount, request);

                // order 
                var shippingMethodMapper = _mapper.Map<ShippingMethod>(request);
                shippingMethodMapper.EstimatedDeliveryDays = 5;
                shippingMethodMapper.Cost = 20;

                var orderItemmapping = _mapper.Map<List<OrderItem>>(cartItems);
                var order = new Order()
                {
                    UserID = user,
                    Status = "pending",
                    OrderIdPaymob = createOrderResponse.Id,
                    TotalAmount = totalAmount,
                    OrderDate = DateTime.UtcNow,
                    OrderItems = orderItemmapping,
                    ShippingMethod = shippingMethodMapper,
                    Payment = new Payment()
                    {
                        Amount = totalAmount,
                        PaymentDate = DateTime.UtcNow,
                    }
                };
                await _orderRepository.AddAsync(order);

                await trans.CommitAsync();
                return Success(keyResponse);
            }
            catch (Exception e)
            {
                await _cartRepository.RollBackAsync();
                return InternalServerError<string>(e.Message);
            }
        }

        public async Task<Response<string>> Handle(OrderCallbackCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var trans = await _cartRepository.BeginTransactionAsync();

                var order = await _orderRepository.GetTableNoTracking().Include(e => e.Payment).FirstOrDefaultAsync(e => e.OrderIdPaymob == request.obj.order.id);
                if (order == null) { return NotFound<string>(); }
                order.Status = request.obj.success ? "paid" : "failure";
                order.Payment.PaymentStatus = request.obj.success ? "paid" : "failure";
                order.Payment.PaymentDate = DateTime.UtcNow;
                order.Payment.PaymentMethod = "card";
                await _orderRepository.UpdateAsync(order);

                var cartExists = await _cartRepository.GetTableNoTracking().FirstOrDefaultAsync(e => e.UserID == order.UserID);
                if (cartExists == null)
                {
                    return NotFound<string>("Cart not found.");
                }
                var cartItems = await _cartItemsRepository.GetTableNoTracking().Where(e => e.CartID == cartExists.CartID).ToListAsync();
                await _cartItemsRepository.DeleteRangeAsync(cartItems);
                await trans.CommitAsync();
                return Success("successfully");
            }
            catch (Exception e)
            {
                await _cartRepository.RollBackAsync();
                return InternalServerError<string>(e.Message);
            }

        }
    }
}
