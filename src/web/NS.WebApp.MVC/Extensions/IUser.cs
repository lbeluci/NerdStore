using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace NS.WebApp.MVC.Extensions
{
    public interface IUser
    {
        ClaimsPrincipal User { get; }

        string Name { get; }

        Guid GetUserId();

        string GetUserEmail();

        string GetUserToken();

        bool IsAuthenticated();

        bool HasRole(string role);

        IEnumerable<Claim> GetClaims();

        HttpContext GetHttpContext();
    }
}
