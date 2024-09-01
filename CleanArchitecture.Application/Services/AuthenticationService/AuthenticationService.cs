using CleanArchitecture.Application.ResultHandler;
using CleanArchitecture.Domain.Entities.Identity;
using CleanArchitecture.Domain.Exceptions;
using CleanArchitecture.Domain.Helper;
using CleanArchitecture.Infrastructure.Repositories.RefreshTokenRepository;
using CleanArchitecture.Infrastructure.Repositories.UserRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using UnauthorizedAccessException = CleanArchitecture.Domain.Exceptions.UnauthorizedAccessException;

namespace CleanArchitecture.Application.Services.AuthenticationService
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly JwtSettings _jwtSettings;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IUserRepository _userRepository;

        public AuthenticationService(JwtSettings jwtSettings, IRefreshTokenRepository refreshTokenRepository, IUserRepository userRepository)
        {
            _jwtSettings = jwtSettings;
            _refreshTokenRepository = refreshTokenRepository;
            _userRepository = userRepository;
        }

        public async Task<AuthJwtResult> GetJWTToken(User user)
        {

            var (jwtToken, accessToken) = await GenerateJWTToken(user);
            var refreshToken = GetRefreshTokenInital(user.UserName);

            var userRefreshToken = new UserRefreshToken
            {
                AddedTime = DateTime.Now,
                ExpiryDate = DateTime.Now.AddDays(_jwtSettings.RefreshTokenExpireDate),
                IsUsed = true,
                IsRevoked = false,
                JwtId = jwtToken.Id,
                RefreshToken = refreshToken.TokenString,
                Token = accessToken,
                UserId = user.Id
            };
            await _refreshTokenRepository.AddAsync(userRefreshToken);

            var response = new AuthJwtResult();
            response.AccessToken = accessToken;
            response.RefreshToken = refreshToken;
            return response;

        }
        private async Task<(JwtSecurityToken, string)> GenerateJWTToken(User user)
        {
            var claims = await GetClaims(user);
            var jwtToken = new JwtSecurityToken(
                _jwtSettings.Issuer,
                _jwtSettings.Audience,
                claims,
                expires: DateTime.Now.AddDays(_jwtSettings.AccessTokenExpireDate),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret)), SecurityAlgorithms.HmacSha256Signature)
                );
            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return (jwtToken, accessToken);
        }
        public async Task<List<Claim>> GetClaims(User user)
        {
            var roles = await _userRepository.GetUserManager().GetRolesAsync(user);

            var claims = new List<Claim>()
            {
                new Claim(nameof(UserClaimModel.Id), user.Id),
                new Claim(ClaimTypes.Name , user.FullName),
                new Claim(ClaimTypes.NameIdentifier,user.UserName),
                new Claim(ClaimTypes.Email, user.Email ),
                new Claim(nameof(UserClaimModel.PhoneNumber), user.PhoneNumber),

            };
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var userClaims = await _userRepository.GetUserManager().GetClaimsAsync(user);
            claims.AddRange(userClaims);
            return claims;
        }
        private RefreshToken GetRefreshTokenInital(string username)
        {
            var refreshToken = new RefreshToken
            {
                ExpireAt = DateTime.Now.AddDays(_jwtSettings.RefreshTokenExpireDate),
                UserName = username,
                TokenString = GenerateRefreshToken()
            };
            return refreshToken;

        }
        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            var randomNumberGenerate = RandomNumberGenerator.Create();
            randomNumberGenerate.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public JwtSecurityToken ReadAccessJWTToken(string accessToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var response = handler.ReadJwtToken(accessToken);
            return response;
        }

        public async Task<(string, DateTime)> validateDetails(JwtSecurityToken jwtToken, string accessToken, string refreshToken)
        {
            if (jwtToken == null || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature)) { throw new BadRequestException("jwtToken is Null or Algoritm is wrong"); }
            if (jwtToken.ValidTo > DateTime.UtcNow)
            {
                throw new UnauthorizedAccessException("Token is not expired");
            }
            string userId = jwtToken.Claims.FirstOrDefault(e => e.Type == nameof(UserClaimModel.Id)).Value;
            var userRefreshToken = await _refreshTokenRepository.GetTableNoTracking()
                                              .FirstOrDefaultAsync(x => x.Token == accessToken &&
                                                                      x.RefreshToken == refreshToken &&
                                                                      x.UserId == userId);
            if (userRefreshToken == null)
            {
                throw new UnauthorizedAccessException("Refresh token is not found");
            }
            if (userRefreshToken.ExpiryDate < DateTime.UtcNow)
            {
                userRefreshToken.IsRevoked = true;
                userRefreshToken.IsUsed = false;
                await _refreshTokenRepository.UpdateAsync(userRefreshToken);
                throw new UnauthorizedAccessException("RefreshTokenIsExpired");
            }
            return (userId, userRefreshToken.ExpiryDate);
        }
        public async Task<AuthJwtResult> GetRefreshToken(User user, JwtSecurityToken jwtToken, DateTime? expiryDate, string refreshToken)
        {
            var (jwtSecurityToken, newAccessToken) = await GenerateJWTToken(user);
            var response = new AuthJwtResult();
            response.AccessToken = newAccessToken;
            var refreshTokenResult = new RefreshToken();
            refreshTokenResult.UserName = jwtToken.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            refreshTokenResult.TokenString = refreshToken;
            refreshTokenResult.ExpireAt = (DateTime)expiryDate;
            response.RefreshToken = refreshTokenResult;
            return response;
        }
        public bool ValidateToken(string accessToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var parameters = new TokenValidationParameters
            {
                ValidateIssuer = _jwtSettings.ValidateIssuer,
                ValidIssuers = new[] { _jwtSettings.Issuer },
                ValidateIssuerSigningKey = _jwtSettings.ValidateIssuerSigningKey,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret)),
                ValidAudience = _jwtSettings.Audience,
                ValidateAudience = _jwtSettings.ValidateAudience,
                ValidateLifetime = _jwtSettings.ValidateLifeTime,
            };
            try
            {
                var validator = handler.ValidateToken(accessToken, parameters, out SecurityToken validatedToken);

                if (validator == null)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
