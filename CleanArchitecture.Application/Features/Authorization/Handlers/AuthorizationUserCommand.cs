using CleanArchitecture.Application.Features.Authorization.Commands.Requests;
using CleanArchitecture.Application.ResultHandler;
using CleanArchitecture.Infrastructure.Repositories.RoleRepository;
using CleanArchitecture.Infrastructure.Repositories.UserRepository;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Data;
using System.Security.Claims;

namespace CleanArchitecture.Application.Features.Authorization.Handlers
{
    public class AuthorizationUserCommand : ResponseHandler,
        IRequestHandler<AddRoleCommand, Response<string>>,
        IRequestHandler<EditRoleCommand, Response<string>>,
        IRequestHandler<DeleteRoleCommand, Response<string>>,
        IRequestHandler<UpdateUserRoleCommand, Response<string>>,
        IRequestHandler<UpdateUserClaimsCommand, Response<string>>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRepository _userRepository;

        public AuthorizationUserCommand(IRoleRepository roleRepository, IUserRepository userRepository)
        {
            _roleRepository = roleRepository;
            _userRepository = userRepository;
        }

        public async Task<Response<string>> Handle(AddRoleCommand request, CancellationToken cancellationToken)
        {
            var roleExist = await _roleRepository.GetRoleManager().RoleExistsAsync(request.RoleName);
            if (roleExist) return BadRequest<string>("role is exists");
            var result = await _roleRepository.GetRoleManager().CreateAsync(new IdentityRole(roleName: request.RoleName));

            if (!result.Succeeded)
            {
                return BadRequest<string>(string.Join("-", result.Errors.Select(x => x.Description).ToList()));
            }
            return Success("Create role success");
        }

        public async Task<Response<string>> Handle(EditRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _roleRepository.GetRoleManager().FindByIdAsync(request.RoleId);
            if (role == null) return NotFound<string>("role not found");

            var roleExist = await _roleRepository.GetRoleManager().RoleExistsAsync(request.RoleName);
            if (roleExist && (role.Name != request.RoleName)) return BadRequest<string>("role is exists");
            role.Name = request.RoleName;
            var result = await _roleRepository.GetRoleManager().UpdateAsync(role);

            if (!result.Succeeded)
            {
                return BadRequest<string>(string.Join("-", result.Errors.Select(x => x.Description).ToList()));
            }
            return Success("Edit role success");
        }
        public async Task<Response<string>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _roleRepository.GetRoleManager().FindByIdAsync(request.RoleId);
            if (role == null) return NotFound<string>("role not found");
            var result = await _roleRepository.GetRoleManager().DeleteAsync(role);

            if (!result.Succeeded)
            {
                return BadRequest<string>(string.Join("-", result.Errors.Select(x => x.Description).ToList()));
            }
            return Success("Delete role success");
        }

        public async Task<Response<string>> Handle(UpdateUserRoleCommand request, CancellationToken cancellationToken)
        {
            var trans = await _userRepository.BeginTransactionAsync();
            try
            {

                var user = await _userRepository.GetUserManager().FindByIdAsync(request.UserId);
                if (user == null) return NotFound<string>("user not found");

                var oldRoles = await _userRepository.GetUserManager().GetRolesAsync(user);
                var removeResult = await _userRepository.GetUserManager().RemoveFromRolesAsync(user, oldRoles);
                if (!removeResult.Succeeded)
                {
                    return BadRequest<string>(string.Join("-", removeResult.Errors.Select(x => x.Description).ToList()));
                }
                var selectRoles = request.RoleResults.Where(e => e.HasRole == true).Select(e => e.RoleName).ToList();
                var result = await _userRepository.GetUserManager().AddToRolesAsync(user, selectRoles);
                if (!result.Succeeded)
                {
                    return BadRequest<string>(string.Join("-", result.Errors.Select(x => x.Description).ToList()));
                }
                await _userRepository.CommitAsync();
                return Success($"User add new roles successfully");
            }
            catch
            {
                await _userRepository.RollBackAsync();
                return InternalServerError<string>();
            }
        }

        public async Task<Response<string>> Handle(UpdateUserClaimsCommand request, CancellationToken cancellationToken)
        {
            var trans = await _userRepository.BeginTransactionAsync();
            try
            {

                var user = await _userRepository.GetUserManager().FindByIdAsync(request.UserId);
                if (user == null) return NotFound<string>("user not found");

                var oldClaims = await _userRepository.GetUserManager().GetClaimsAsync(user);
                var removeResult = await _userRepository.GetUserManager().RemoveClaimsAsync(user, oldClaims);
                if (!removeResult.Succeeded)
                {
                    return BadRequest<string>(string.Join("-", removeResult.Errors.Select(x => x.Description).ToList()));
                }
                var listClaims = new List<Claim>();
                foreach (var selectRole in request.UserClaims)
                {
                    if (selectRole.HasClaim)
                        listClaims.Add(new Claim(selectRole.ClaimName, selectRole.HasClaim.ToString()));
                }
                var result = await _userRepository.GetUserManager().AddClaimsAsync(user, listClaims);
                if (!result.Succeeded)
                {
                    return BadRequest<string>(string.Join("-", result.Errors.Select(x => x.Description).ToList()));
                }
                await _userRepository.CommitAsync();
                return Success($"User add new claims successfully");
            }
            catch
            {
                await _userRepository.RollBackAsync();
                return InternalServerError<string>();
            }
        }
    }
}
