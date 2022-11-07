namespace AuthService.Repositories;

public interface IAuthRepository
{
    Task<string> GetUserPasswordByUsernameAsync(string username);
}
