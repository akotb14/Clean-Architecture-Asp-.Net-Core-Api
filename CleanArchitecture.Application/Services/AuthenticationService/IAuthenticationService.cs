using CleanArchitecture.Application.ResultHandler;
using CleanArchitecture.Domain.Entities.Identity;
using System.IdentityModel.Tokens.Jwt;

namespace CleanArchitecture.Application.Services.AuthenticationService
{
    public interface IAuthenticationService
    {
        Task<AuthJwtResult> GetJWTToken(User user);
        JwtSecurityToken ReadAccessJWTToken(string accessToken);
        Task<(string, DateTime)> validateDetails(JwtSecurityToken jwtToken, string accessToken, string refreshToken);
        Task<AuthJwtResult> GetRefreshToken(User user, JwtSecurityToken jwtToken, DateTime? expiryDate, string refreshToken);
        bool ValidateToken(string accessToken);
    }
}
