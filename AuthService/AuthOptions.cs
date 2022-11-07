using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace AuthService;

public class AuthOptions
{
    // Required NuGet package: 
    // "Microsoft.AspNetCore.Authentication.JwtBearer" Version="6.0.10"

    public const string ISSUER = "RestaurantCatalogAuth";
    public const string AUDIENCE = "AuthClient";
    const string KEY = "superSecretKeyForTestTask";
    public const int LIFETIME = 720;
    public static SymmetricSecurityKey GetSymmetricSecurityKey()
    {
        return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
    }
}
