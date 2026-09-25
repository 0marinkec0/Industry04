using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Infrastructure.Identity;

public class User : IdentityUser<int>
{
    public static User Create(string email, string userName)
    {
        return new User { UserName = userName, Email = email };
    }

    public static List<Claim> GetClaims(User user)
    {
        return new List<Claim>
         {
            new Claim(ClaimTypes.Name, user.UserName!),
            new Claim(ClaimTypes.Email, user.Email!),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
         };
    }
}
