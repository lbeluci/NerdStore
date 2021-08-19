using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace NS.WebApp.MVC.Extensions
{
    public class AspNetUser : IUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AspNetUser(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public ClaimsPrincipal User => _httpContextAccessor.HttpContext.User;

        public string Name => User.Identity.Name;

        public Guid GetUserId()
        {
            return IsAuthenticated() ? Guid.Parse(User.GetUserId()) : Guid.Empty;
        }

        public string GetUserEmail()
        {
            return IsAuthenticated() ? User.GetUserEmail() : "";
        }

        public string GetUserToken()
        {
            return IsAuthenticated() ? User.GetUserToken() : "";
        }

        public bool IsAuthenticated()
        {
            return User.Identity.IsAuthenticated;
        }

        public bool HasRole(string role)
        {
            return User.IsInRole(role);
        }

        public IEnumerable<Claim> GetClaims()
        {
            return User.Claims;
        }

        public HttpContext GetHttpContext()
        {
            return _httpContextAccessor.HttpContext;
        }
    }
}