using CleanArchitecture.Application.Features.Authorization.Queries.Requests;
using CleanArchitecture.Application.Features.Authorization.Queries.Response;
using CleanArchitecture.Application.ResultHandler;
using CleanArchitecture.Domain.Helper;
using CleanArchitecture.Infrastructure.Repositories.RoleRepository;
using CleanArchitecture.Infrastructure.Repositories.UserRepository;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Features.Authorization.Handlers
{
    public class AuthorizationUserQuery : ResponseHandler,
        IRequestHandler<GetRolesQuery, Response<List<IdentityRole>>>,
        IRequestHandler<GetRoleByIdQuery, Response<IdentityRole>>,
        IRequestHandler<GetUserRolesQuery, Response<GetUserRolesResponse>>,
        IRequestHandler<GetUserClaimsQuery, Response<GetUserClaimsResponse>>

    {
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRepository _userRepository;
        public AuthorizationUserQuery(IRoleRepository roleRepository, IUserRepository userRepository)
        {
            _roleRepository = roleRepository;
            _userRepository = userRepository;
        }

        public async Task<Response<List<IdentityRole>>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
        {
            var roles = await _roleRepository.GetRoleManager().Roles.ToListAsync();
            return Success(roles);
        }

        public async Task<Response<IdentityRole>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            var role = await _roleRepository.GetRoleManager().FindByIdAsync(request.RoleId);
            if (role == null) return NotFound<IdentityRole>("role not found");
            return Success(role);
        }

        public async Task<Response<GetUserRolesResponse>> Handle(GetUserRolesQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserManager().FindByIdAsync(request.UserId);
            if (user == null) return NotFound<GetUserRolesResponse>("user not found");
            var response = new GetUserRolesResponse();
            response.UserId = user.Id;
            var roles = await _roleRepository.GetRoleManager().Roles.ToListAsync();
            var rolesList = new List<RoleResults>();
            foreach (var role in roles)
            {
                var roleResonse = new RoleResults();
                roleResonse.RoleId = role.Id;
                roleResonse.RoleName = role.Name;
                roleResonse.HasRole = await _userRepository.GetUserManager().IsInRoleAsync(user, role.Name) ? true : false;

                rolesList.Add(roleResonse);
            }
            response.RoleResults = rolesList;
            return Success(response);
        }

        public async Task<Response<GetUserClaimsResponse>> Handle(GetUserClaimsQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserManager().FindByIdAsync(request.UserId);
            if (user == null) return NotFound<GetUserClaimsResponse>("user not found");
            var response = new GetUserClaimsResponse();
            response.UserId = user.Id;
            var claimsUser = await _userRepository.GetUserManager().GetClaimsAsync(user);
            var claimsList = new List<UserClaims>();
            foreach (var claim in ClaimsStore.claims)
            {
                var claimResonse = new UserClaims();
                claimResonse.ClaimName = claim.Type;
                var checkClaims = claimsUser.Any(e => e.Type.Equals(claim.Type));
                if (checkClaims)
                {
                    claimResonse.HasClaim = true;
                }
                else
                {
                    claimResonse.HasClaim = false;
                }
                claimsList.Add(claimResonse);
            }
            response.UserClaims = claimsList;
            return Success(response);
        }
    }
}
