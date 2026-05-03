using AutoMapper;
using CleanArchitecture.Application.Features.Categories.Commands.Requests;
using CleanArchitecture.Application.ResultHandler;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Infrastructure.Repositories.CategoryRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Features.Categories.Handlers
{
    public class CategoryCommandHandler : ResponseHandler,
        IRequestHandler<AddCategoryCommad, Response<string>>,
        IRequestHandler<EditCategoryCommad, Response<string>>,
        IRequestHandler<DeleteCategoryCommad, Response<string>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(AddCategoryCommad request, CancellationToken cancellationToken)
        {
            var checkCagetory = await _categoryRepository.GetTableNoTracking().FirstOrDefaultAsync(e => e.CategoryName == request.CategoryName);
            if (checkCagetory != null) { return BadRequest<string>("Category is exists"); }
            var category = _mapper.Map<Category>(request);
            var result = await _categoryRepository.AddAsync(category);
            if (result == null)
            {
                return BadRequest<string>("can't create category");
            }
            return Success("Create category successfully");
        }

        public async Task<Response<string>> Handle(EditCategoryCommad request, CancellationToken cancellationToken)
        {
            var checkCagetory = await _categoryRepository.GetByIdAsync(request.CategoryID);
            if (checkCagetory == null) { return NotFound<string>("Category not found"); }
            var category = _mapper.Map(request, checkCagetory);
            var result = await _categoryRepository.UpdateAsync(category);
            if (result == null)
            {
                return BadRequest<string>("can't update category");
            }
            return Success("Edit category successfully");
        }

        public async Task<Response<string>> Handle(DeleteCategoryCommad request, CancellationToken cancellationToken)
        {
            var cagetory = await _categoryRepository.GetByIdAsync(request.CategoryID);
            if (cagetory == null) { return NotFound<string>("Category not found"); }

            var result = await _categoryRepository.DeleteAsync(cagetory);
            if (result == null)
            {
                return BadRequest<string>("can't delete category");
            }
            return Success("delete Category successfully");
        }
    }
}
