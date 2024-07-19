using Microsoft.AspNetCore.Identity;

namespace DemoApp.API.Interfaces
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
