using CleanArchitecture.Application.Features.Authentication.Commands.Requests;
using CleanArchitecture.Application.ResultHandler;
using CleanArchitecture.Application.Services.AuthenticationService;
using CleanArchitecture.Application.Services.CurrentUserService;
using CleanArchitecture.Application.Services.EmailsService;
using CleanArchitecture.Domain.Entities.Identity;
using CleanArchitecture.Infrastructure.Repositories.UserRepository;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace CleanArchitecture.Application.Features.Authentication.Handlers
{
    public class AuthenticationHandlerCommand : ResponseHandler,
        IRequestHandler<SignInCommand, Response<AuthJwtResult>>,
        IRequestHandler<RefreshTokenCommand, Response<AuthJwtResult>>,
        IRequestHandler<SendResetPasswordCommand, Response<string>>,
        IRequestHandler<ResetPasswordCommand, Response<string>>

    {
        private readonly IUserRepository _userRepository;
        private readonly SignInManager<User> _signInManager;
        private readonly IAuthenticationService _authenticationService;
        private readonly IEmailsService _emailsService;
        private readonly ICurrentUserService _currentUserService;

        public AuthenticationHandlerCommand(IUserRepository userRepository, SignInManager<User> signInManager, IAuthenticationService authenticationService, IEmailsService emailsService, ICurrentUserService currentUserService)
        {
            _userRepository = userRepository;
            _signInManager = signInManager;
            _authenticationService = authenticationService;
            _emailsService = emailsService;
            _currentUserService = currentUserService;
        }

        public async Task<Response<AuthJwtResult>> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserManager().FindByEmailAsync(request.Email);
            if (user == null) { return NotFound<AuthJwtResult>("Email or Password not correct"); }

            if (!user.EmailConfirmed) { return Unauthorized<AuthJwtResult>("Email not active. go to mail and verify it"); }

            var checkPassword = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (!checkPassword.Succeeded) { return BadRequest<AuthJwtResult>("Email or Password not correct"); }


            var jwtAuthTokens = await _authenticationService.GetJWTToken(user);
            return Success(jwtAuthTokens);
        }

        public async Task<Response<AuthJwtResult>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            //read token previous
            var readToken = _authenticationService.ReadAccessJWTToken(request.AccessToken);
            //validate token 
            var (userId, expireAt) = await _authenticationService.validateDetails(readToken, request.AccessToken, request.RefreshToken);

            var user = await _userRepository.GetUserManager().FindByIdAsync(userId);
            if (user == null) return NotFound<AuthJwtResult>(" user not found");

            var currentUserId = _currentUserService.GetUserId();
            if (user.Id != currentUserId) return NotFound<AuthJwtResult>("you are not right user");

            var result = await _authenticationService.GetRefreshToken(user, readToken, expireAt, request.RefreshToken);
            return Success(result);
        }

        public async Task<Response<string>> Handle(SendResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var trans = await _userRepository.BeginTransactionAsync();
            try
            {
                var user = await _userRepository.GetUserManager().FindByEmailAsync(request.Email);
                if (user == null) { return NotFound<string>("Email not found"); }

                var currentUserId = _currentUserService.GetUserId();
                if (user.Id != currentUserId) return NotFound<string>("you are not right user");

                var chars = "0123456789";
                var random = new Random();
                var randomNumber = new string(Enumerable.Repeat(chars, 6).Select(s => s[random.Next(s.Length)]).ToArray());
                user.Code = randomNumber;

                var updateResult = await _userRepository.GetUserManager().UpdateAsync(user);
                if (!updateResult.Succeeded) { return BadRequest<string>(); }

                await _emailsService.SendEmail(user.Email, user.Code, "Reset password");
                await _userRepository.CommitAsync();
                return Success("send code in email");
            }
            catch
            {
                await _userRepository.RollBackAsync();
                return InternalServerError<string>();
            }
        }

        public async Task<Response<string>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var trans = await _userRepository.BeginTransactionAsync();
            try
            {

                var user = await _userRepository.GetUserManager().FindByEmailAsync(request.Email);
                if (user == null) { return NotFound<string>("Email not found"); }

                var currentUserId = _currentUserService.GetUserId();
                if (user.Id != currentUserId) return NotFound<string>("you are not right user");

                if (user.Code != request.Code) { return BadRequest<string>("Invalid code"); }
                await _userRepository.GetUserManager().RemovePasswordAsync(user);
                if (!await _userRepository.GetUserManager().HasPasswordAsync(user))
                {
                    await _userRepository.GetUserManager().AddPasswordAsync(user, request.Password);
                }
                await _userRepository.CommitAsync();
                return Success("change password successfully");
            }
            catch
            {
                await _userRepository.RollBackAsync();
                return InternalServerError<string>();
            }
        }
    }
}
