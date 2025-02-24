using The_Engneering.Authentication;
using The_Engneering.Contracts.Authentication;

namespace The_Engneering.Services;

public class AuthService(UserManager<ApplicationUser> _userManager, IJwtProvider _jwtProvider) : IAuthService
{
    private readonly UserManager<ApplicationUser> userManager = _userManager;
    private readonly IJwtProvider jwtProvider = _jwtProvider;

    public async Task<AuthResponse?> GetTokenAsync(string email, string password, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(email);
        if (user == null) 
            return null;

        var IsPassValid = await userManager.CheckPasswordAsync(user, password);
        if (!IsPassValid) 
            return null;

        var(token, expiresIn) = jwtProvider.GenerateToken(user);

        return new AuthResponse(user.Id, user.Email, user.FirstName, user.LastName, token, expiresIn);
    }
}
