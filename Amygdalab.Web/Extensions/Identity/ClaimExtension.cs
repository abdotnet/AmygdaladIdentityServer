using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Amygdalab.Web.Extensions.Identity
{
    public static class ClaimExtension
    {
        public static long GetUserId(this ClaimsPrincipal principal)
        {
            try
            {
                if (principal == null)
                    throw new ArgumentNullException(nameof(principal));

                string userId = principal.FindFirst("userId")?.Value;

                return long.Parse(userId);
            }
            catch
            {
                return 0;
            }
        }
    }
}
