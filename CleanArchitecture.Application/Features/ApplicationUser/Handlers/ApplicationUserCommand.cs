using AutoMapper;
using CleanArchitecture.Application.Features.ApplicationUser.Commands.Requests;
using CleanArchitecture.Application.ResultHandler;
using CleanArchitecture.Application.Services.CurrentUserService;
using CleanArchitecture.Application.Services.EmailsService;
using CleanArchitecture.Application.Services.FileService;
using CleanArchitecture.Domain.Entities.Identity;
using CleanArchitecture.Infrastructure.Repositories.UserRepository;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Application.Features.ApplicationUser.Handlers
{
    public class ApplicationUserCommand : ResponseHandler,
        IRequestHandler<AddUserCommand, Response<string>>,
        IRequestHandler<UpdateUserCommand, Response<string>>,
        IRequestHandler<DeleteUserCommand, Response<string>>,
        IRequestHandler<ChangePasswordCommand, Response<string>>,
        IRequestHandler<EditImageProfileRequest, Response<string>>

    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailsService _emailsService;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUrlHelper _urlHelper;
        private readonly IFileService _fileService;
        private readonly ICurrentUserService _currentUserService;
        public ApplicationUserCommand(IUserRepository userRepository, IMapper mapper, IEmailsService emailService, IEmailsService emailsService, IHttpContextAccessor httpContextAccessor, IUrlHelper urlHelper, IFileService fileService, ICurrentUserService currentUserService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _emailsService = emailService;
            _emailsService = emailsService;
            _httpContextAccessor = httpContextAccessor;
            _urlHelper = urlHelper;
            _fileService = fileService;
            _currentUserService = currentUserService;
        }

        public async Task<Response<string>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var beginTrans = await _userRepository.BeginTransactionAsync();
            try
            {
                var checkemail = await _userRepository.GetUserManager().FindByEmailAsync(request.Email);
                var checkuser = await _userRepository.GetUserManager().FindByNameAsync(request.UserName);
                if (checkuser != null || checkemail != null) { return BadRequest<string>("email or user is exist"); }

                var userMapper = _mapper.Map<User>(request);

                var userResult = await _userRepository.GetUserManager().CreateAsync(userMapper, request.Password);
                if (!userResult.Succeeded)
                {
                    return BadRequest<string>(string.Join("-", userResult.Errors.Select(x => x.Description).ToList()));
                }

                await _userRepository.GetUserManager().AddToRoleAsync(userMapper, "User");

                var code = await _userRepository.GetUserManager().GenerateEmailConfirmationTokenAsync(userMapper);
                var resquestAccessor = _httpContextAccessor.HttpContext?.Request;
                var returnUrl = resquestAccessor?.Scheme + "://" + resquestAccessor?.Host + _urlHelper.Action("ConfirmEmail", "Authentication", new { UserId = userMapper.Id, Code = code });
                var message = $"To Confirm Email Click Link: <a href='{returnUrl}'>Link Of Confirmation</a>";

                await _emailsService.SendEmail(request.Email, message, "Confirm your Account");

                await _userRepository.CommitAsync();
                return Created("Success");
            }
            catch
            {
                await _userRepository.RollBackAsync();
                return InternalServerError<string>();


            }
        }

        public async Task<Response<string>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var checkUser = await _userRepository.GetUserManager().FindByIdAsync(request.UserId);
            if (checkUser == null)
            {
                return NotFound<string>("User not found");
            }
            var result = await _userRepository.GetUserManager().DeleteAsync(checkUser);
            if (!result.Succeeded)
            {
                return BadRequest<string>(string.Join("-", result.Errors.Select(e => e.Description).ToList()));
            }
            return Success("user delete successfully");
        }

        public async Task<Response<string>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserManager().FindByIdAsync(request.Id);
            if (user == null) { return NotFound<string>("User not found"); }

            var currentUserId = _currentUserService.GetUserId();
            if (user.Id != currentUserId) return NotFound<string>("you are not right user");

            var checkemail = await _userRepository.GetUserManager().FindByEmailAsync(request.Email);
            var checkuserName = await _userRepository.GetUserManager().FindByNameAsync(request.UserName);

            if ((checkuserName != null && user.Id != checkuserName.Id) || (checkemail != null && user.Id != checkemail.Id))
            {
                return BadRequest<string>("email or user is exist");
            }
            var userMapping = _mapper.Map(request, user);
            var result = await _userRepository.GetUserManager().UpdateAsync(userMapping);
            if (!result.Succeeded)
            {
                return BadRequest<string>(string.Join("-", result.Errors.Select(e => e.Description).ToList()));
            }
            return Success("user update successfully");

        }

        public async Task<Response<string>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserManager().FindByIdAsync(request.UserId);
            if (user == null) { return NotFound<string>("User not found"); }

            var currentUserId = _currentUserService.GetUserId();
            if (user.Id != currentUserId) return NotFound<string>("you are not right user");

            var result = await _userRepository.GetUserManager().ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
            if (!result.Succeeded)
            {
                return BadRequest<string>(string.Join("-", result.Errors.Select(e => e.Description).ToList()));
            }
            return Success("change password successfully");
        }

        public async Task<Response<string>> Handle(EditImageProfileRequest request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserManager().FindByIdAsync(request.UserId);
            if (user == null) { return NotFound<string>("User not found"); }

            var currentUserId = _currentUserService.GetUserId();
            if (user.Id != currentUserId) return NotFound<string>("you are not right user");

            var file = request.ImageProfile;
            var reqContext = _httpContextAccessor.HttpContext.Request;
            var baseUrl = reqContext.Scheme + "://" + reqContext.Host;
            var imageUrl = await _fileService.UploadFile("User-Image", file);
            user.ImageProfile = baseUrl + imageUrl;
            var result = await _userRepository.GetUserManager().UpdateAsync(user);
            if (!result.Succeeded)
            {
                return BadRequest<string>(string.Join("-", result.Errors.Select(e => e.Description).ToList()));
            }
            return Success("change ImageProfile successfully");

        }
    }
}
