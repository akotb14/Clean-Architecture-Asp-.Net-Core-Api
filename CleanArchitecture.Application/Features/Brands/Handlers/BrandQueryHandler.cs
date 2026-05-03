using AutoMapper;
using CleanArchitecture.Application.Features.Brands.Queries.Requests;
using CleanArchitecture.Application.Features.Brands.Queries.Responses;
using CleanArchitecture.Application.ResultHandler;
using CleanArchitecture.Infrastructure.Repositories.BrandRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Features.Brands.Handlers
{
    public class BrandQueryHandler : ResponseHandler,
        IRequestHandler<GetBrandQuery, Response<List<GetBrandResponse>>>,
        IRequestHandler<GetBrandByIdQuery, Response<GetBrandResponse>>
    {
        private readonly IBrandRepository _BrandRepository;
        private readonly IMapper _mapper;

        public BrandQueryHandler(IBrandRepository BrandRepository, IMapper mapper)
        {
            _BrandRepository = BrandRepository;
            _mapper = mapper;
        }
        public async Task<Response<List<GetBrandResponse>>> Handle(GetBrandQuery request, CancellationToken cancellationToken)
        {
            var categories = await _BrandRepository.GetTableNoTracking().ToListAsync();
            var cagetoryMapping = _mapper.Map<List<GetBrandResponse>>(categories);
            return Success(cagetoryMapping);
        }

        public async Task<Response<GetBrandResponse>> Handle(GetBrandByIdQuery request, CancellationToken cancellationToken)
        {
            var cagetory = await _BrandRepository.GetByIdAsync(request.BrandID);
            if (cagetory == null) { return BadRequest<GetBrandResponse>("Brand not found"); }
            var cagetoryMapping = _mapper.Map<GetBrandResponse>(cagetory);
            return Success(cagetoryMapping);
        }
    }
}
