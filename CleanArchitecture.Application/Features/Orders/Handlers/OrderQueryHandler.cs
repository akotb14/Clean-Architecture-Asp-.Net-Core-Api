using AutoMapper;
using CleanArchitecture.Application.Features.Orders.Queries.Requests;
using CleanArchitecture.Application.Features.Orders.Queries.Responses;
using CleanArchitecture.Application.ResultHandler;
using CleanArchitecture.Application.Services.CurrentUserService;
using CleanArchitecture.Infrastructure.Repositories.OrderItemsRepository;
using CleanArchitecture.Infrastructure.Repositories.OrderRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Features.Orders.Handlers
{
    public class OrderQueryHandler : ResponseHandler,
        IRequestHandler<GetOrdersUserQuery, Response<List<GetOrdersResponse>>>
    {

        private readonly IOrderRepository _orderRepository;
        private readonly IOrderItemsRepository _orderItemsRepository;
        private readonly ICurrentUserService _currentUser;
        private readonly IMapper _mapper;

        public OrderQueryHandler(IOrderRepository orderRepository, IOrderItemsRepository orderItemsRepository, ICurrentUserService currentUser, IMapper mapper)
        {

            _orderRepository = orderRepository;
            _orderItemsRepository = orderItemsRepository;
            _currentUser = currentUser;
            _mapper = mapper;
        }

        public async Task<Response<List<GetOrdersResponse>>> Handle(GetOrdersUserQuery request, CancellationToken cancellationToken)
        {
            //get user 
            var userID = _currentUser.GetUserId();
            // order
            var orders = await _orderRepository.GetTableNoTracking().Where(e => e.UserID == userID).ToListAsync();
            var orderItems = _orderItemsRepository.GetTableNoTracking();
            //foreach (var order in orders)
            //{
            //    orderItems.Where(e => e.OrderID == order.OrderID);
            //}
            var orderItemsList = await orderItems.Include(e => e.Order).Include(e => e.Product)
                .Where(e => e.Order.UserID == userID).ToListAsync();

            //mapper
            var ordersMapper = _mapper.Map<List<GetOrdersResponse>>(orderItemsList);
            return Success(ordersMapper);
        }
    }
}
