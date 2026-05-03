using AutoMapper;
using CleanArchitecture.Application.Features.ShoppingCarts.Commads.Requests;
using CleanArchitecture.Application.ResultHandler;
using CleanArchitecture.Application.Services.CurrentUserService;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Infrastructure.Repositories.CartItemsRepository;
using CleanArchitecture.Infrastructure.Repositories.CartRepository;
using CleanArchitecture.Infrastructure.Repositories.ProductRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Features.ShoppingCarts.Handlers
{
    public class CartCommandHandler : ResponseHandler,
        IRequestHandler<AddCartCommand, Response<string>>,
        IRequestHandler<AddCartItemsCommand, Response<string>>,
        IRequestHandler<UpdateCartItemsCommand, Response<string>>,
        IRequestHandler<RemoveCartItemCommand, Response<string>>,
        IRequestHandler<ClearCartItemsCommand, Response<string>>
    {
        private readonly ICartRepository _cartRepository;
        private readonly ICartItemsRepository _cartItemsRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public CartCommandHandler(ICartRepository cartRepository, ICartItemsRepository cartItemsRepository, ICurrentUserService currentUserService, IMapper mapper, IProductRepository productRepository)
        {
            _cartRepository = cartRepository;
            _cartItemsRepository = cartItemsRepository;
            _currentUserService = currentUserService;
            _mapper = mapper;
            _productRepository = productRepository;
        }

        public async Task<Response<string>> Handle(AddCartCommand request, CancellationToken cancellationToken)
        {
            var userID = _currentUserService.GetUserId();
            var cartExists = await _cartRepository.GetTableNoTracking()
                                                  .FirstOrDefaultAsync(e => e.UserID == userID, cancellationToken);

            if (cartExists != null)
            {
                return BadRequest<string>("Cart already exists.");
            }

            var newCart = new ShoppingCart
            {
                UserID = userID,
                CreatedDate = DateTime.UtcNow,
                ModifiedDate = null
            };

            await _cartRepository.AddAsync(newCart);
            return Success("Shopping cart added successfully.");
        }

        public async Task<Response<string>> Handle(AddCartItemsCommand request, CancellationToken cancellationToken)
        {
            var userID = _currentUserService.GetUserId();
            var cartExists = await _cartRepository.GetTableNoTracking()
                                                  .FirstOrDefaultAsync(e => e.UserID == userID);

            if (cartExists == null)
            {
                return BadRequest<string>("Cart not found.");
            }

            var productExists = await _productRepository.GetByIdAsync(request.ProductID);
            if (productExists == null)
            {
                return NotFound<string>("Product not found.");
            }
            var existingCartItems = _cartItemsRepository.GetTableAsTracking();
            if (productExists.Sizes.Count() > 0)
            {
                existingCartItems = existingCartItems
                 .Where(ci => ci.CartID == cartExists.CartID && ci.ProductID == request.ProductID && ci.Size == request.Size);
            }
            else
            {
                existingCartItems = existingCartItems
               .Where(ci => ci.CartID == cartExists.CartID && ci.ProductID == request.ProductID);
            }
            // Check if the cart item already exists in the cart
            var existingCartItem = await existingCartItems.FirstOrDefaultAsync();
            if (existingCartItem != null)
            {
                // If the item exists, update the quantity
                existingCartItem.Quantity += request.Quantity;
                existingCartItem.UnitPrice = productExists.Price; // Optionally update the unit price if required
                await _cartItemsRepository.UpdateAsync(existingCartItem);
            }
            else
            {
                // If the item does not exist, add it as a new cart item
                var newCartItem = _mapper.Map<CartItem>(request);
                newCartItem.CartID = cartExists.CartID;
                newCartItem.UnitPrice = productExists.Price;
                await _cartItemsRepository.AddAsync(newCartItem);
            }

            return Success("Cart item processed successfully.");
        }

        public async Task<Response<string>> Handle(UpdateCartItemsCommand request, CancellationToken cancellationToken)
        {
            var existingCartItem = await _cartItemsRepository.GetByIdAsync(request.CartItemId);

            if (existingCartItem == null)
            {
                return NotFound<string>("cart item not found");
            }
            existingCartItem.Quantity = request.Quantity;
            if (existingCartItem.Quantity < 1) { return BadRequest<string>("quantity cant change to negetive"); }
            await _cartItemsRepository.UpdateAsync(existingCartItem);
            return Success("update quantity success");
        }

        public async Task<Response<string>> Handle(RemoveCartItemCommand request, CancellationToken cancellationToken)
        {
            var existingCartItem = await _cartItemsRepository.GetByIdAsync(request.CartItemId);

            if (existingCartItem == null)
            {
                return NotFound<string>("cart item not found");
            }
            await _cartItemsRepository.DeleteAsync(existingCartItem);
            return Success("delete item successfully");
        }

        public async Task<Response<string>> Handle(ClearCartItemsCommand request, CancellationToken cancellationToken)
        {
            var userID = _currentUserService.GetUserId();
            var cartExists = await _cartRepository.GetTableNoTracking().FirstOrDefaultAsync(e => e.UserID == userID);
            if (cartExists == null)
            {
                return NotFound<string>("Cart not found.");
            }
            var cartItems = await _cartItemsRepository.GetTableNoTracking().Where(e => e.CartID == cartExists.CartID).ToListAsync();
            await _cartItemsRepository.DeleteRangeAsync(cartItems);
            return Success("Cart items cleared successfully.");
        }
    }
}
