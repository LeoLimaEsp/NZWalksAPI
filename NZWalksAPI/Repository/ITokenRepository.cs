using Microsoft.AspNetCore.Identity;

namespace NZWalksAPI.Repository
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
