using Entity.DbContexts.Catalog;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Repositories;

public class AuthRepository : IAuthRepository
{
    private readonly CatalogDbContext _context;

    public AuthRepository(CatalogDbContext context)
    {
        _context = context;
    }
    
    public async Task<string> GetUserPasswordByUsernameAsync(string username)
    {
        var userPassword = await _context.Users
            .Where(s => s.UserName == username)
            .Select(s => s.Password)
            .FirstOrDefaultAsync();

        return userPassword;
    }
}
