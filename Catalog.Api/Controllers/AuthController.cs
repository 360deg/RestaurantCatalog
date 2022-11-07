using AuthService.Models;
using AuthService.Services;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController
{
    private readonly IAuthService _authService;
    
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
    
    /// <summary>
    /// Returns access_token for user if request successfully passed.
    /// </summary>
    /// <param name="request">User login and password</param>
    /// <remarks>
    ///{
    ///    "userName": "Test",
    ///    "password": "Test"
    ///} 
    /// </remarks>
    [HttpPost]
    [Route("[action]")]
    public async Task<string> Token([FromBody] TokenRequest request)
    {
        return await _authService.GetAccessTokenAsync(request.UserName, request.Password);
    }
}
