using The_Engneering.Contracts.Authentication;

namespace The_Engneering.Services;

public interface IAuthService
{
    Task<AuthResponse?> GetTokenAsync(string email, string password, CancellationToken cancellationToken);
}
