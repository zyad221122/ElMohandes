
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace The_Engneering.Authentication;

public class JwtProvider : IJwtProvider
{
    public (string token, int expiresIn) GenerateToken(ApplicationUser user)
    {
        // Generate claims
        Claim[] claims = [
            new(JwtRegisteredClaimNames.Sub, user.Id),
            new(JwtRegisteredClaimNames.Email, user.Email!),
            new(JwtRegisteredClaimNames.GivenName, user.FirstName!),
            new(JwtRegisteredClaimNames.FamilyName, user.LastName!),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        ];

        var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("O1YjDQOryMIeS1yMtUfobV61DcH5QmoR"));
        var singingCredentials = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256);

        var expiresIn = 30;
        var expirationDate = DateTime.UtcNow.AddMinutes(expiresIn);

        var token = new JwtSecurityToken(
            issuer : "EngineeringApp",
            audience: "EngineeringApp Users",
            claims: claims,
            expires: expirationDate,
            signingCredentials: singingCredentials
        );

        return (new JwtSecurityTokenHandler().WriteToken(token), expiresIn * 60);
    }
}
