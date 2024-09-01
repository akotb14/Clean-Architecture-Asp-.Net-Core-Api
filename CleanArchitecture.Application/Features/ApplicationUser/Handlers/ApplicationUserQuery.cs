using AutoMapper;
using CleanArchitecture.Application.CustomExtensionMethod;
using CleanArchitecture.Application.Features.ApplicationUser.Queries.Requests;
using CleanArchitecture.Application.Features.ApplicationUser.Queries.Response;
using CleanArchitecture.Application.ResultHandler;
using CleanArchitecture.Infrastructure.Repositories.UserRepository;
using MediatR;

namespace CleanArchitecture.Application.Features.ApplicationUser.Handlers
{
    public class ApplicationUserQuery : ResponseHandler,
        IRequestHandler<GetUserByIdQuery, Response<GetUserReponseQuery>>,
        IRequestHandler<GetUserPaginationQuery, PaginatedResult<GetUserReponseQuery>>

    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public ApplicationUserQuery(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Response<GetUserReponseQuery>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserManager().FindByIdAsync(request.UserId);
            if (user == null) { return NotFound<GetUserReponseQuery>(); }
            var userMapping = _mapper.Map<GetUserReponseQuery>(user);
            return Success(userMapping);
        }

        public async Task<PaginatedResult<GetUserReponseQuery>> Handle(GetUserPaginationQuery request, CancellationToken cancellationToken)
        {
            var users = _userRepository.GetTableNoTracking().AsQueryable();

            var search = request.FilterSearch;
            if (search != null)
            {
                users = users.Where(e => e.FullName.Contains(search) || e.Email.Contains(search) || e.Address.Contains(search) || e.UserName.Contains(search) || e.PhoneNumber.Contains(search));
            }
            users = _userRepository.OrderUser(users, request.OrderBy, request.OrderByDirection);
            var userMapperPaginated = await _mapper.ProjectTo<GetUserReponseQuery>(users).ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return userMapperPaginated;

        }
    }
}
