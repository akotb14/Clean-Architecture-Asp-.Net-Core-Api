using AutoMapper;
using CleanArchitecture.Application.Features.Categories.Queries.Requests;
using CleanArchitecture.Application.Features.Categories.Queries.Responses;
using CleanArchitecture.Application.ResultHandler;
using CleanArchitecture.Infrastructure.Repositories.CategoryRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Features.Categories.Handlers
{
    public class CategoryQueryHandler : ResponseHandler,
        IRequestHandler<GetCategoriesQuery, Response<List<GetCategoryResponse>>>,
        IRequestHandler<GetCategoryByIdQuery, Response<GetCategoryResponse>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryQueryHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        public async Task<Response<List<GetCategoryResponse>>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = await _categoryRepository.GetTableNoTracking().ToListAsync();
            var cagetoryMapping = _mapper.Map<List<GetCategoryResponse>>(categories);
            return Success(cagetoryMapping);
        }

        public async Task<Response<GetCategoryResponse>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var cagetory = await _categoryRepository.GetByIdAsync(request.CategoryID);
            if (cagetory == null) { return BadRequest<GetCategoryResponse>("Category not found"); }
            var cagetoryMapping = _mapper.Map<GetCategoryResponse>(cagetory);
            return Success(cagetoryMapping);
        }
    }
}
