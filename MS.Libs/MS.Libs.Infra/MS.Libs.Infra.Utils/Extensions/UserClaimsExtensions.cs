using System.Security.Claims;
using System.Security.Principal;

namespace MS.Libs.Infra.Utils.Extensions;

public static class UserClaimsExtensions
{
    public static string GetUserClaim(this IIdentity Identity, string claim)
    {
        if (Identity == null)
        {
            return "";
        }
        var claimsIdentity = Identity as ClaimsIdentity;
        return claimsIdentity.FindFirst(claim).ToString();
    }
}
