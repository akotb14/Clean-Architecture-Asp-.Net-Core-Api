using AutoMapper;
using CleanArchitecture.Application.Features.ShoppingCarts.Queries.Requests;
using CleanArchitecture.Application.Features.ShoppingCarts.Queries.Responses;
using CleanArchitecture.Application.ResultHandler;
using CleanArchitecture.Application.Services.CurrentUserService;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Infrastructure.Repositories.CartItemsRepository;
using CleanArchitecture.Infrastructure.Repositories.CartRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Features.ShoppingCarts.Handlers
{
    public class CartQueryHandler : ResponseHandler,
        IRequestHandler<GetCartAndCartItemsByUserIdQuery, Response<List<GetCartandItemsResponse>>>,
        IRequestHandler<GetCartQuery, Response<ShoppingCart>>,
        IRequestHandler<GetTotalQuantityQuery, Response<int>>
    {
        private readonly ICartRepository _cartRepository;
        private readonly ICartItemsRepository _CartItemsRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;
        public CartQueryHandler(ICartRepository cartRepository, ICartItemsRepository cartItemsRepository, ICurrentUserService currentUserService, IMapper mapper)
        {
            _cartRepository = cartRepository;
            _CartItemsRepository = cartItemsRepository;
            _currentUserService = currentUserService;
            _mapper = mapper;
        }
        public async Task<Response<List<GetCartandItemsResponse>>> Handle(GetCartAndCartItemsByUserIdQuery request, CancellationToken cancellationToken)
        {
            var userID = _currentUserService.GetUserId();
            var cart = await _cartRepository.GetTableNoTracking().FirstOrDefaultAsync(e => e.UserID == userID);
            if (cart == null) { return NotFound<List<GetCartandItemsResponse>>("Cart not found"); }
            var cartItem = await _CartItemsRepository.GetTableNoTracking().Where(e => e.CartID == cart.CartID).Include(e => e.Product).ToListAsync();
            var cartItemMapping = _mapper.Map<List<GetCartandItemsResponse>>(cartItem);
            return Success(cartItemMapping);
        }



        public async Task<Response<ShoppingCart>> Handle(GetCartQuery request, CancellationToken cancellationToken)
        {
            var userID = _currentUserService.GetUserId();
            var cart = await _cartRepository.GetTableNoTracking().FirstOrDefaultAsync(e => e.UserID == userID);
            if (cart == null) { return NotFound<ShoppingCart>("Cart not found"); }
            return Success(cart);

        }

        public async Task<Response<int>> Handle(GetTotalQuantityQuery request, CancellationToken cancellationToken)
        {
            var userID = _currentUserService.GetUserId();
            var cart = await _cartRepository.GetTableNoTracking().FirstOrDefaultAsync(e => e.UserID == userID);
            if (cart == null) { return NotFound<int>("Cart not found"); }
            int totalQuantity = await _CartItemsRepository.SumQuantity(cart.CartID);
            return Success(totalQuantity);

        }
    }
}
