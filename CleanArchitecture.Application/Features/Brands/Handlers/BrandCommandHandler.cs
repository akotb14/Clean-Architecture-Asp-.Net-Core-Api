using AutoMapper;
using CleanArchitecture.Application.Features.Brands.Commands.Requests;
using CleanArchitecture.Application.ResultHandler;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Infrastructure.Repositories.BrandRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Features.Brands.Handlers
{
    public class BrandCommandHandler : ResponseHandler,
        IRequestHandler<AddBrandCommad, Response<string>>,
        IRequestHandler<EditBrandCommad, Response<string>>,
        IRequestHandler<DeleteBrandCommad, Response<string>>
    {
        private readonly IBrandRepository _BrandRepository;
        private readonly IMapper _mapper;

        public BrandCommandHandler(IBrandRepository BrandRepository, IMapper mapper)
        {
            _BrandRepository = BrandRepository;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(AddBrandCommad request, CancellationToken cancellationToken)
        {
            var checkBrand = await _BrandRepository.GetTableNoTracking().FirstOrDefaultAsync(e => e.BrandName == request.BrandName);
            if (checkBrand != null) { return BadRequest<string>("Brand is exists"); }
            var Brand = _mapper.Map<Brand>(request);
            var result = await _BrandRepository.AddAsync(Brand);
            if (result == null)
            {
                return BadRequest<string>("can't create Brand");
            }
            return Success("Create Brand successfully");
        }

        public async Task<Response<string>> Handle(EditBrandCommad request, CancellationToken cancellationToken)
        {
            var checkBrand = await _BrandRepository.GetByIdAsync(request.BrandID);
            if (checkBrand == null) { return NotFound<string>("Brand not found"); }
            var Brand = _mapper.Map(request, checkBrand);
            var result = await _BrandRepository.UpdateAsync(Brand);
            if (result == null)
            {
                return BadRequest<string>("can't update Brand");
            }
            return Success("Edit Brand successfully");
        }

        public async Task<Response<string>> Handle(DeleteBrandCommad request, CancellationToken cancellationToken)
        {
            var Brand = await _BrandRepository.GetByIdAsync(request.BrandID);
            if (Brand == null) { return NotFound<string>("Brand not found"); }

            var result = await _BrandRepository.DeleteAsync(Brand);
            if (result == null)
            {
                return BadRequest<string>("can't delete Brand");
            }
            return Success("delete brand successfully");
        }
    }
}
