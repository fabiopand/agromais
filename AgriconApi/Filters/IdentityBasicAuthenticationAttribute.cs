using AgriconApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;

namespace AgriconApi.Filters
{
    public class IdentityBasicAuthenticationAttribute : BasicAuthenticationAttribute
    {
        private AgriconWebApiEntities db = new AgriconWebApiEntities();

        protected override async Task<IPrincipal> AuthenticateAsync(string userName, string password, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            
            Produtor produtor = db.Produtor.FirstOrDefault(w => w.CPF == userName && w.Senha == password);

            if (produtor == null)
            {
                // No user with userName/password exists.
                return null;
            }

            // Create a ClaimsIdentity with all the claims for this user.
            Claim nameClaim = new Claim(ClaimTypes.Name, userName);
            List<Claim> claims = new List<Claim> { nameClaim };

            // important to set the identity this way, otherwise IsAuthenticated will be false
            // see: http://leastprivilege.com/2012/09/24/claimsidentity-isauthenticated-and-authenticationtype-in-net-4-5/
            //ClaimsIdentity identity = new ClaimsIdentity(claims, AuthenticationTypes.Basic);
            ClaimsIdentity identity = new ClaimsIdentity(claims, "Custom");

            var principal = new ClaimsPrincipal(identity);
            return principal;
        }

    }
}