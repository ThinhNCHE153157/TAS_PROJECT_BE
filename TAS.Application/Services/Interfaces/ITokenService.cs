using System.Security.Claims;

namespace TAS.Application.Services.Interfaces
{
    public interface ITokenService
    {
        public string GenerateAccessToken(IEnumerable<Claim> claims);
        public ClaimsPrincipal GetPrincipalFromExpriedToken(string token);
    }
}
