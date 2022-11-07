namespace AuthService.Services;

public interface IAuthService
{
    Task<string> GetAccessTokenAsync(string username, string password);
}
