using Microsoft.AspNetCore.Identity;

namespace NZWalksAPI.Repository
{
    public class TokenRepository : ITokenRepository
    {
        public string CreateJWTToken(IdentityUser user, List<string> roles)
        {
            //  Create claims
        }
    }
}
