using Softcode.GTex.ApploicationService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Softcode.GTex.ExceptionHelper;

namespace Softcode.GTex.Api.Providers
{
    public class AuthorizeFilter : IAuthorizationFilter
    {
        private readonly RoleRight RoleRight;
        private readonly ILoggedInUserService LoggedInUserService;
        private readonly IApplicationServiceBuilder ApplicationServiceBuilder;

        public AuthorizeFilter(RoleRight roleRight, ILoggedInUserService loggedInUserService, IApplicationServiceBuilder applicationServiceBuilder)
        {
            RoleRight = roleRight;
            LoggedInUserService = loggedInUserService;
            ApplicationServiceBuilder = applicationServiceBuilder;
        }

        public ILoggedInUser LoggedInUser => LoggedInUserService.LoggedInUser;

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                throw new SoftcodeForbiddenException("Not authorize");
            }

            if (!string.IsNullOrWhiteSpace(RoleRight.Roles))
            {
                string[] roles = RoleRight.Roles.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (roles.Length == 0)
                {
                    throw new SoftcodeForbiddenException("Not authorize");
                }

                if (!LoggedInUser.RoleIds.Any(x => roles.Contains(x)))
                {
                    throw new SoftcodeForbiddenException("Not authorize");
                }
                return;
            }

            if (RoleRight.Right != 0)
            {
                ILoggedInUserService loggedInUserService = context.HttpContext.RequestServices.GetService<ILoggedInUserService>() as ILoggedInUserService;
                ApplicationServiceBuilder.PermissionService.Initialize(loggedInUserService.LoggedInUser, ApplicationServiceBuilder.ApplicationCacheService);
                //TODO
                if (!ApplicationServiceBuilder.PermissionService.IsValid(RoleRight.Right))
                {
                   throw new SoftcodeForbiddenException("Not authorize");
                }
                return;
            }

            throw new SoftcodeForbiddenException("Not authorize");
        }
    }

    public class RoleRight
    {
        public string Roles { get; set; }
        public int Right { get; set; }
    }


    public class ActionAuthorizeAttribute : TypeFilterAttribute
    {
        public ActionAuthorizeAttribute(int Right = 0, string Roles = "") : base(typeof(AuthorizeFilter))
        {
            Arguments = new object[]
            {
                new RoleRight
                {
                    Roles = Roles,
                    Right = Right
                }
            };
        }
    }
}
