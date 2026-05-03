using AutoMapper;
using CleanArchitecture.Application.Features.Products.Commands.Requests;
using CleanArchitecture.Application.ResultHandler;
using CleanArchitecture.Application.Services.FileService;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Infrastructure.Repositories.BrandRepository;
using CleanArchitecture.Infrastructure.Repositories.CategoryRepository;
using CleanArchitecture.Infrastructure.Repositories.ProductRepository;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace CleanArchitecture.Application.Features.Products.Handlers
{
    public class ProductCommandHandler : ResponseHandler,
        IRequestHandler<AddProductCommad, Response<string>>,
        IRequestHandler<EditProductCommad, Response<string>>,
        IRequestHandler<DeleteProductCommad, Response<string>>
    {
        private readonly IProductRepository _ProductRepository;
        private readonly ICategoryRepository _CategoryRepository;
        private readonly IBrandRepository _BrandRepository;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProductCommandHandler(IProductRepository ProductRepository, IMapper mapper, ICategoryRepository categoryRepository, IBrandRepository brandRepository, IFileService fileService, IHttpContextAccessor httpContextAccessor)
        {
            _ProductRepository = ProductRepository;
            _mapper = mapper;
            _CategoryRepository = categoryRepository;
            _BrandRepository = brandRepository;
            _fileService = fileService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Response<string>> Handle(AddProductCommad request, CancellationToken cancellationToken)
        {
            var checkCategory = await _CategoryRepository.GetByIdAsync(request.CategoryID);
            var checkBrand = await _BrandRepository.GetByIdAsync(request.BrandID);
            if (checkCategory == null || checkBrand == null) { return BadRequest<string>("Brand or Cagetory are not found"); }

            var image = request.ImageURL;

            var reqContext = _httpContextAccessor.HttpContext.Request;
            var baseUrl = reqContext.Scheme + "://" + reqContext.Host;

            var imageUrl = await _fileService.UploadFile("", image);


            var Product = _mapper.Map<Product>(request);
            Product.ImageURL = baseUrl + imageUrl;
            var result = await _ProductRepository.AddAsync(Product);
            if (result == null)
            {
                return BadRequest<string>("can't create Product");
            }
            return Success("Create Product successfully");
        }

        public async Task<Response<string>> Handle(EditProductCommad request, CancellationToken cancellationToken)
        {
            var checkProduct = await _ProductRepository.GetByIdAsync(request.ProductID);
            if (checkProduct == null) { return NotFound<string>("Product not found"); }

            var checkCategory = await _CategoryRepository.GetByIdAsync(request.CategoryID);
            var checkBrand = await _BrandRepository.GetByIdAsync(request.BrandID);
            if (checkCategory == null || checkBrand == null) { return BadRequest<string>("Brand or Cagetory are not found"); }

            var Product = _mapper.Map(request, checkProduct);
            var result = await _ProductRepository.UpdateAsync(Product);
            if (result == null)
            {
                return BadRequest<string>("can't update Product");
            }
            return Success("Edit Product successfully");
        }

        public async Task<Response<string>> Handle(DeleteProductCommad request, CancellationToken cancellationToken)
        {
            var cagetory = await _ProductRepository.GetByIdAsync(request.ProductID);
            if (cagetory == null) { return NotFound<string>("Product not found"); }

            var result = await _ProductRepository.UpdateAsync(cagetory);
            if (result == null)
            {
                return BadRequest<string>("can't delete Product");
            }
            return Success("delete Product successfully");
        }
    }
}
