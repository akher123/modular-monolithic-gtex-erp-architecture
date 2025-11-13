using IdentityModel;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Softcode.GTex.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Softcode.GTex.Api
{
    public class AdditionalClaimsProfileService : IProfileService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly IUserClaimsPrincipalFactory<ApplicationUser> userClaimsFactory;
        private readonly IHttpContextAccessor httpContextAccessor;

        public AdditionalClaimsProfileService(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IUserClaimsPrincipalFactory<ApplicationUser> userClaimsFactory, IHttpContextAccessor httpContextAccessor)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.userClaimsFactory = userClaimsFactory;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var val = httpContextAccessor.HttpContext.Response.Headers;

            string sub = context.Subject.GetSubjectId();
            ApplicationUser user = await userManager.FindByIdAsync(sub);
            IEnumerable<Claim> roleClaims = context.Subject.FindAll(JwtClaimTypes.Role);
            ClaimsPrincipal principal = await userClaimsFactory.CreateAsync(user);
            IList<Claim> userClaims = await userManager.GetClaimsAsync(user);

            List<Claim> claims = principal.Claims.ToList();
            claims = claims.Where(claim => context.RequestedClaimTypes.Contains(claim.Type)).ToList();
            claims.Add(new Claim(JwtClaimTypes.GivenName, user.UserName));
            claims.AddRange(userClaims);

            //claims.Add(new Claim(ApplicationConstants.BUSSINESS_PROFILE_ID_CLAIM, "1"));
            //claims.Add(new Claim(IdentityServerConstants.StandardScopes.Email, user.Email));



            context.IssuedClaims = claims;
            context.IssuedClaims.AddRange(roleClaims);
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            string sub = context.Subject.GetSubjectId();
            ApplicationUser user = await userManager.FindByIdAsync(sub);
            context.IsActive = user != null;
        }
    }
}
