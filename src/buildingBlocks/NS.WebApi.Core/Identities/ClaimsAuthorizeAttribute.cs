using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace NS.WebApi.Core.Identities
{
    public class ClaimsAuthorizeAttribute : TypeFilterAttribute
    {
        public ClaimsAuthorizeAttribute(string claimName, string claimValue) : base(typeof(ClaimsAuthorizeFilter))
        {
            Arguments = new object[] { new Claim(claimName, claimValue) };
        }
    }
}