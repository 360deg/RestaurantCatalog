using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AuthService.Repositories;
using Common.Exceptions;
using Microsoft.IdentityModel.Tokens;

namespace AuthService.Services;

public class AuthService : IAuthService
{
    private readonly IAuthRepository _authRepository;
    
    public AuthService(IAuthRepository authRepository)
    {
        _authRepository = authRepository;
    }

    private async Task<ClaimsIdentity?> GetIdentityAsync(string username, string password)
    {
        var userPassword = await _authRepository.GetUserPasswordByUsernameAsync(username);
        
        if (!HashPasswordGenerator.VerifyHash(userPassword, password))
        {
            return null;
        }

        var claimsIdentity = new ClaimsIdentity(null, "Token", ClaimsIdentity.DefaultNameClaimType,
            ClaimsIdentity.DefaultRoleClaimType);
            
        return claimsIdentity;
    }

    public async Task<string> GetAccessTokenAsync(string username, string password)
    {
        var identity = await GetIdentityAsync(username, password);
        if (identity == null)
        {
            throw new BadRequestException("Invalid username or password.");
        }
 
        var now = DateTime.UtcNow;
        
        var jwt = new JwtSecurityToken(
            issuer: AuthOptions.ISSUER,
            audience: AuthOptions.AUDIENCE,
            notBefore: now,
            claims: identity.Claims,
            expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
        
        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

        return encodedJwt;
    }
}
