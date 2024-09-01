﻿using CleanArchitecture.Application.Features.Authentication.Queries.Requests;
using CleanArchitecture.Application.ResultHandler;
using CleanArchitecture.Application.Services.AuthenticationService;
using CleanArchitecture.Infrastructure.Repositories.UserRepository;
using MediatR;

namespace CleanArchitecture.Application.Features.Authentication.Handlers
{
    public class AuthenticationHandlerQuery : ResponseHandler,
        IRequestHandler<ConfirmEmailQuery, Response<string>>,
        IRequestHandler<ConfirmResetPasswordQuery, Response<string>>,
        IRequestHandler<AuthorizeUserQuery, Response<string>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthenticationService _authenticationService;
        public AuthenticationHandlerQuery(IUserRepository userRepository, IAuthenticationService authenticationService)
        {
            _userRepository = userRepository;
            _authenticationService = authenticationService;
        }

        public async Task<Response<string>> Handle(ConfirmEmailQuery request, CancellationToken cancellationToken)
        {

            var user = await _userRepository.GetUserManager().FindByIdAsync(request.UserId);
            if (user == null) { return NotFound<string>(); }
            var confirmEmail = await _userRepository.GetUserManager().ConfirmEmailAsync(user, request.Code);
            if (!confirmEmail.Succeeded)
            {
                return BadRequest<string>(string.Join("-", confirmEmail.Errors.Select(x => x.Description).ToList()));
            }
            return Success("Confirm Email success");
        }

        public async Task<Response<string>> Handle(ConfirmResetPasswordQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserManager().FindByEmailAsync(request.Email);
            if (user == null) { return NotFound<string>("Email not found"); }
            if (user.Code != request.Code) { return BadRequest<string>("Code is invalid"); }
            return Success("Confirm reset password success");

        }

        public async Task<Response<string>> Handle(AuthorizeUserQuery request, CancellationToken cancellationToken)
        {
            var result = Task.FromResult(_authenticationService.ValidateToken(request.AccessToken));
            if (!await result) { return Unauthorized<string>("Token is invalid"); }
            return Success("token is valid");
        }
    }
}