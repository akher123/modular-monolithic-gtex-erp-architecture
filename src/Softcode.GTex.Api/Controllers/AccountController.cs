using IdentityModel;
using IdentityServer4.Events;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Softcode.GTex.Api.Models;
using Softcode.GTex.ApplicantionService.Messaging;
using Softcode.GTex.ApplicantionService.Messaging.Models;
using Softcode.GTex.ApploicationService;
using Softcode.GTex.Configuration;
using Softcode.GTex.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace Softcode.GTex.Api.Controllers
{
    /// <summary>
    /// This sample controller implements a typical login/logout/provision workflow for local and external accounts.
    /// The login service encapsulates the interactions with the user data store. This data store is in-memory only and cannot be used for production!
    /// The interaction service provides a way for the UI to communicate with identityserver for validation and context retrieval
    /// </summary>
    [SecurityHeaders]
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IIdentityServerInteractionService interaction;
        private readonly IClientStore clientStore;
        private readonly IAuthenticationSchemeProvider schemeProvider;
        private readonly IEventService events;
        private readonly IApplicationCacheService applicationCacheService;
        private readonly IEmailJobQueueService emailJobQueueService;
        private readonly ISecurityProfileService securityProfileService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="signInManager"></param>
        /// <param name="interaction"></param>
        /// <param name="clientStore"></param>
        /// <param name="schemeProvider"></param>
        /// <param name="events"></param>
        /// <param name="applicationCacheService"></param>
        /// <param name="emailJobQueueService"></param>
        /// <param name="securityProfileService"></param>
        public AccountController(
            UserManager<ApplicationUser> userManager
            , SignInManager<ApplicationUser> signInManager
            , IIdentityServerInteractionService interaction
            , IClientStore clientStore
            , IAuthenticationSchemeProvider schemeProvider
            , IEventService events
            , IApplicationCacheService applicationCacheService
            , IEmailJobQueueService emailJobQueueService
            , ISecurityProfileService securityProfileService
            )
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.interaction = interaction;
            this.clientStore = clientStore;
            this.schemeProvider = schemeProvider;
            this.events = events;
            this.applicationCacheService = applicationCacheService;
            this.emailJobQueueService = emailJobQueueService;
            this.securityProfileService = securityProfileService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl)
        {
            LoginViewModel loginViewModel = await BuildLoginViewModelAsync(returnUrl);
            return View(loginViewModel);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="button"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginInputModel model, string button)
        {
            if (button != "login")
            {
                // the user clicked the "cancel" button
                AuthorizationRequest context = await interaction.GetAuthorizationContextAsync(model.ReturnUrl);
                if (context != null)
                {
                    // if the user cancels, send a result back into IdentityServer as if they 
                    // denied the consent (even if this client does not require consent).
                    // this will send back an access denied OIDC error response to the client.
                    await interaction.GrantConsentAsync(context, ConsentResponse.Denied);

                    // we can trust model.ReturnUrl since GetAuthorizationContextAsync returned non-null
                    return Redirect(model.ReturnUrl);
                }
                else
                {
                    // since we don't have a valid context, then we just go back to the home page
                    return Redirect("~/");
                }
            }

            LoginViewModel loginViewModel = await BuildLoginViewModelAsync(model);

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("isInvalidModel", "Invalid data!");
                return View(loginViewModel);
            }

            //signInManager.Options.Lockout.DefaultLockoutTimeSpan = 

            ApplicationUser applicationUser = userManager.Users.Where(u => u.UserName == model.Username && u.IsActive)
                //.Include(u => u.Contact)
                .Include(b => b.UserBusinessProfiles)
                .ThenInclude(x => x.BusinessProfile)
                .Include(x=>x.SecurityProfile)
                .FirstOrDefault();

            if (applicationUser == null)
            {
                ModelState.AddModelError("invalidCredentials", AccountOptions.InvalidCredentialsErrorMessage);
                return View(loginViewModel);
            }

            if (applicationUser.AdminLocked)
            {
                ModelState.AddModelError("isLockedOut", AccountOptions.LoginInvalidAccountLockedEnd);
                return View(loginViewModel);
            }

            signInManager.Options.Lockout.MaxFailedAccessAttempts = applicationUser.SecurityProfile.NumberOfAttemptsBeforeLockout + 1;

            signInManager.Options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(applicationUser.SecurityProfile.LockoutDuration);

            SignInResult result = await signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberLogin, lockoutOnFailure: true);

            if (result.IsLockedOut)
            {
                ModelState.AddModelError("isLockedOut", AccountOptions.LoginInvalidAccountLockedEnd);
                return View(loginViewModel);
            }

            if (result.Succeeded)
            {
                if (applicationUser.UserBusinessProfiles.Count > 1)
                {
                    string userId = Encryption.Encrypt(applicationUser.Id);
                    return RedirectToAction("BusinessProfile", new { token = userId, returnUrl = model.ReturnUrl });
                }

                BusinessProfile businessProfile = applicationUser.UserBusinessProfiles.FirstOrDefault().BusinessProfile;

                if (businessProfile != null)
                {
                    await events.RaiseAsync(new UserLoginFailureEvent(model.Username, "invalid user information"));
                    ModelState.AddModelError("invalidCredentials", "invalid user information");
                }

                var userClaims = await userManager.GetClaimsAsync(applicationUser);

                await userManager.RemoveClaimsAsync(applicationUser, userClaims);

                await userManager.AddClaimAsync(applicationUser, new Claim(ApplicationConstants.BusinessProfileIdClaim, businessProfile.Id.ToString()));

                if (!applicationUser.LockoutEnabled)
                {
                    await events.RaiseAsync(new UserLoginFailureEvent(model.Username, AccountOptions.LoginInvalidAccountLockedForever));
                }

                await events.RaiseAsync(new UserLoginSuccessEvent(applicationUser.UserName, applicationUser.Id.ToString(), applicationUser.UserName));

                // make sure the returnUrl is still valid, and if so redirect back to authorize endpoint or a local page
                // the IsLocalUrl check is only necessary if you want to support additional local pages, otherwise IsValidReturnUrl is more strict
                if (interaction.IsValidReturnUrl(model.ReturnUrl) || Url.IsLocalUrl(model.ReturnUrl))
                {
                    return Redirect(model.ReturnUrl);
                }

                return Redirect("~/");
            }

            if (applicationUser.AccessFailedCount > 3)
            {
                loginViewModel.EnableCaptcha = true;
            }

            ModelState.AddModelError("", AccountOptions.InvalidCredentialsErrorMessage);
            return View(loginViewModel);
        }

        /// <summary>
        /// Show logout page
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Logout(string logoutId)
        {
            // build a model so the logout page knows what to display
            LogoutViewModel vm = await BuildLogoutViewModelAsync(logoutId);

            if (vm.ShowLogoutPrompt == false)
            {
                // if the request for logout was properly authenticated from IdentityServer, then
                // we don't need to show the prompt and can just log the user out directly.
                return await Logout(vm, "");
            }

            return View(vm);
        }

        /// <summary>
        /// Handle logout page postback
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout(LogoutInputModel model, string returnUrl)
        {
            // build a model so the logged out page knows what to display
            LoggedOutViewModel vm = await BuildLoggedOutViewModelAsync(model.LogoutId);

            if (User?.Identity.IsAuthenticated == true)
            {
                // delete local authentication cookie
                await signInManager.SignOutAsync();

                string userId = User.GetSubjectId();
                // raise the logout event
                await events.RaiseAsync(new UserLogoutSuccessEvent(userId, User.GetDisplayName()));

                this.applicationCacheService.RemoveItemByKey(userId);
            }

            // check if we need to trigger sign-out at an upstream identity provider
            if (vm.TriggerExternalSignout)
            {
                // build a return URL so the upstream provider will redirect back
                // to us after the user has logged out. this allows us to then
                // complete our single sign-out processing.
                string url = Url.Action("Logout", new { logoutId = vm.LogoutId });

                // this triggers a redirect to the external provider for sign-out
                return SignOut(new AuthenticationProperties { RedirectUri = url }, vm.ExternalAuthenticationScheme);
            }

            return Redirect(vm.PostLogoutRedirectUri);
            //return View("LoggedOut", vm);
        }

        /*****************************************/
        /* helper APIs for the AccountController */
        /*****************************************/
        private async Task<LoginViewModel> BuildLoginViewModelAsync(string returnUrl)
        {
            AuthorizationRequest context = await interaction.GetAuthorizationContextAsync(returnUrl);
            if (context?.IdP != null)
            {
                // this is meant to short circuit the UI and only trigger the one external IdP
                return new LoginViewModel
                {
                    EnableLocalLogin = false,
                    ReturnUrl = returnUrl,
                    Username = context?.LoginHint,
                };
            }

            IEnumerable<AuthenticationScheme> schemes = await schemeProvider.GetAllSchemesAsync();

            List<ExternalProvider> providers = schemes
                .Where(x => x.DisplayName != null ||
                            (x.Name.Equals(AccountOptions.WindowsAuthenticationSchemeName, StringComparison.OrdinalIgnoreCase))
                )
                .Select(x => new ExternalProvider
                {
                    DisplayName = x.DisplayName,
                    AuthenticationScheme = x.Name
                }).ToList();

            bool allowLocal = true;
            if (context?.ClientId != null)
            {
                Client client = await clientStore.FindEnabledClientByIdAsync(context.ClientId);
                if (client != null)
                {
                    allowLocal = client.EnableLocalLogin;

                    if (client.IdentityProviderRestrictions != null && client.IdentityProviderRestrictions.Any())
                    {
                        providers = providers.Where(provider => client.IdentityProviderRestrictions.Contains(provider.AuthenticationScheme)).ToList();
                    }
                }
            }

            return new LoginViewModel
            {
                AllowRememberLogin = AccountOptions.AllowRememberLogin,
                EnableLocalLogin = allowLocal && AccountOptions.AllowLocalLogin,
                ReturnUrl = returnUrl,
                Username = context?.LoginHint,
            };
        }

        private async Task<LoginViewModel> BuildLoginViewModelAsync(LoginInputModel model)
        {
            LoginViewModel vm = await BuildLoginViewModelAsync(model.ReturnUrl);
            vm.Username = model.Username;
            vm.RememberLogin = model.RememberLogin;
            return vm;
        }

        private async Task<LogoutViewModel> BuildLogoutViewModelAsync(string logoutId)
        {
            LogoutViewModel vm = new LogoutViewModel { LogoutId = logoutId, ShowLogoutPrompt = AccountOptions.ShowLogoutPrompt };

            if (User?.Identity.IsAuthenticated != true)
            {
                // if the user is not authenticated, then just show logged out page
                vm.ShowLogoutPrompt = false;
                return vm;
            }

            LogoutRequest context = await interaction.GetLogoutContextAsync(logoutId);
            if (context?.ShowSignoutPrompt == false)
            {
                // it's safe to automatically sign-out
                vm.ShowLogoutPrompt = false;
                return vm;
            }

            // show the logout prompt. this prevents attacks where the user
            // is automatically signed out by another malicious web page.
            return vm;
        }

        private async Task<LoggedOutViewModel> BuildLoggedOutViewModelAsync(string logoutId)
        {
            // get context information (client name, post logout redirect URI and iframe for federated signout)
            LogoutRequest logout = await interaction.GetLogoutContextAsync(logoutId);

            LoggedOutViewModel vm = new LoggedOutViewModel
            {
                AutomaticRedirectAfterSignOut = AccountOptions.AutomaticRedirectAfterSignOut,
                PostLogoutRedirectUri = logout?.PostLogoutRedirectUri,
                ClientName = string.IsNullOrEmpty(logout?.ClientName) ? logout?.ClientId : logout?.ClientName,
                SignOutIframeUrl = logout?.SignOutIFrameUrl,
                LogoutId = logoutId
            };

            if (User?.Identity.IsAuthenticated == true)
            {
                string idp = User.FindFirst(JwtClaimTypes.IdentityProvider)?.Value;
                if (idp != null && idp != IdentityServer4.IdentityServerConstants.LocalIdentityProvider)
                {
                    bool providerSupportsSignout = await HttpContext.GetSchemeSupportsSignOutAsync(idp);
                    if (providerSupportsSignout)
                    {
                        if (vm.LogoutId == null)
                        {
                            // if there's no current logout context, we need to create one
                            // this captures necessary info from the current logged in user
                            // before we signout and redirect away to the external IdP for signout
                            vm.LogoutId = await interaction.CreateLogoutContextAsync();
                        }

                        vm.ExternalAuthenticationScheme = idp;
                    }
                }
            }

            return vm;
        }

        private async Task<IActionResult> ProcessWindowsLoginAsync(string returnUrl)
        {
            // see if windows auth has already been requested and succeeded
            AuthenticateResult result = await HttpContext.AuthenticateAsync(AccountOptions.WindowsAuthenticationSchemeName);
            if (result?.Principal is WindowsPrincipal wp)
            {
                // we will issue the external cookie and then redirect the
                // user back to the external callback, in essence, tresting windows
                // auth the same as any other external authentication mechanism
                AuthenticationProperties props = new AuthenticationProperties()
                {
                    RedirectUri = Url.Action("ExternalLoginCallback"),
                    Items =
                    {
                        {"returnUrl", returnUrl},
                        {"scheme", AccountOptions.WindowsAuthenticationSchemeName},
                    }
                };

                ClaimsIdentity id = new ClaimsIdentity(AccountOptions.WindowsAuthenticationSchemeName);
                id.AddClaim(new Claim(JwtClaimTypes.Subject, wp.Identity.Name));
                id.AddClaim(new Claim(JwtClaimTypes.Name, wp.Identity.Name));

                // add the groups as claims -- be careful if the number of groups is too large
                if (AccountOptions.IncludeWindowsGroups)
                {
                    WindowsIdentity wi = wp.Identity as WindowsIdentity;
                    IdentityReferenceCollection groups = wi.Groups.Translate(typeof(NTAccount));
                    IEnumerable<Claim> roles = groups.Select(x => new Claim(JwtClaimTypes.Role, x.Value));
                    id.AddClaims(roles);
                }

                await HttpContext.SignInAsync(
                    IdentityServer4.IdentityServerConstants.ExternalCookieAuthenticationScheme,
                    new ClaimsPrincipal(id),
                    props);
                return Redirect(props.RedirectUri);
            }
            else
            {
                // trigger windows auth
                // since windows auth don't support the redirect uri,
                // this URL is re-triggered when we call challenge
                return Challenge(AccountOptions.WindowsAuthenticationSchemeName);
            }
        }

        private async Task<(ApplicationUser user, string provider, string providerUserId, IEnumerable<Claim> claims)> FindUserFromExternalProviderAsync(AuthenticateResult result)
        {
            ClaimsPrincipal externalUser = result.Principal;

            // try to determine the unique id of the external user (issued by the provider)
            // the most common claim type for that are the sub claim and the NameIdentifier
            // depending on the external provider, some other claim type might be used
            Claim userIdClaim = externalUser.FindFirst(JwtClaimTypes.Subject) ??
                                externalUser.FindFirst(ClaimTypes.NameIdentifier) ??
                                throw new Exception("Unknown userid");

            // remove the user id claim so we don't include it as an extra claim if/when we provision the user
            List<Claim> claims = externalUser.Claims.ToList();
            claims.Remove(userIdClaim);

            string provider = result.Properties.Items["scheme"];
            string providerUserId = userIdClaim.Value;

            // find external user
            ApplicationUser user = await userManager.FindByLoginAsync(provider, providerUserId);



            return (user, provider, providerUserId, claims);
        }

        private async Task<ApplicationUser> AutoProvisionUserAsync(string provider, string providerUserId, IEnumerable<Claim> claims)
        {
            // create a list of claims that we want to transfer into our store
            List<Claim> filtered = new List<Claim>();

            // user's display name
            string name = claims.FirstOrDefault(x => x.Type == JwtClaimTypes.Name)?.Value ??
                          claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
            if (name != null)
            {
                filtered.Add(new Claim(JwtClaimTypes.Name, name));
            }
            else
            {
                string first = claims.FirstOrDefault(x => x.Type == JwtClaimTypes.GivenName)?.Value ??
                               claims.FirstOrDefault(x => x.Type == ClaimTypes.GivenName)?.Value;
                string last = claims.FirstOrDefault(x => x.Type == JwtClaimTypes.FamilyName)?.Value ??
                              claims.FirstOrDefault(x => x.Type == ClaimTypes.Surname)?.Value;
                if (first != null && last != null)
                {
                    filtered.Add(new Claim(JwtClaimTypes.Name, first + " " + last));
                }
                else if (first != null)
                {
                    filtered.Add(new Claim(JwtClaimTypes.Name, first));
                }
                else if (last != null)
                {
                    filtered.Add(new Claim(JwtClaimTypes.Name, last));
                }
            }

            // email
            string email = claims.FirstOrDefault(x => x.Type == JwtClaimTypes.Email)?.Value ??
                           claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            if (email != null)
            {
                filtered.Add(new Claim(JwtClaimTypes.Email, email));
            }

            ApplicationUser user = new ApplicationUser
            {
                UserName = Guid.NewGuid().ToString(),
            };
            IdentityResult identityResult = await userManager.CreateAsync(user);
            if (!identityResult.Succeeded) throw new Exception(identityResult.Errors.First().Description);

            if (filtered.Any())
            {
                identityResult = await userManager.AddClaimsAsync(user, filtered);
                if (!identityResult.Succeeded) throw new Exception(identityResult.Errors.First().Description);
            }

            identityResult = await userManager.AddLoginAsync(user, new UserLoginInfo(provider, providerUserId, provider));
            if (!identityResult.Succeeded) throw new Exception(identityResult.Errors.First().Description);

            return user;
        }

        private void ProcessLoginCallbackForOidc(AuthenticateResult externalResult, List<Claim> localClaims, AuthenticationProperties localSignInProps)
        {
            // if the external system sent a session id claim, copy it over
            // so we can use it for single sign-out
            Claim sid = externalResult.Principal.Claims.FirstOrDefault(x => x.Type == JwtClaimTypes.SessionId);
            if (sid != null)
            {
                localClaims.Add(new Claim(JwtClaimTypes.SessionId, sid.Value));
            }

            // if the external provider issued an id_token, we'll keep it for signout
            string id_token = externalResult.Properties.GetTokenValue("id_token");
            if (id_token != null)
            {
                localSignInProps.StoreTokens(new[] { new AuthenticationToken { Name = "id_token", Value = id_token } });
            }
        }

        //
        // GET: /Account/ForgotPassword
        [HttpGet]
        public IActionResult ForgotPassword(string returnUrl)
        {
            ForgotPasswordViewModel vm = new ForgotPasswordViewModel
            {
                ReturnUrl = returnUrl
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model, string button, string returnUrl)
        {
            if (string.IsNullOrWhiteSpace(button) || !button.Equals("send-link", StringComparison.InvariantCulture))
            {
                ModelState.AddModelError("", "Invalid operation");
                return View();
            }

            int startIndex = returnUrl.IndexOf("client_id=") + "client_id=".Length;
            int lastIndex = returnUrl.LastIndexOf("&redirect_uri");

            string clientId = returnUrl.Substring(startIndex, lastIndex-startIndex);

            if (ModelState.IsValid)
            {
                //var user = await _userManager.FindByEmailAsync(model.EmailAddress);
                ApplicationUser user = userManager.Users.Where(u => u.Email == model.EmailAddress && u.IsActive)
                    .Include(u => u.Contact).Include(c => c.UserBusinessProfiles)
                    .FirstOrDefault();
                if (user == null || !(await userManager.IsEmailConfirmedAsync(user)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                string code = await userManager.GeneratePasswordResetTokenAsync(user);

                ResetPasswordToken resetPasswordToken = new ResetPasswordToken
                {
                    UserId = user.Id,
                    Code = code,
                    ClientId = clientId
                };

                var token = Encryption.Encrypt(JsonConvert.SerializeObject(resetPasswordToken));

                var callbackUrl = Url.Action("ResetPassword", "Account", new { token }, protocol: HttpContext.Request.Scheme);

                EmailJobQueueModel email = new EmailJobQueueModel
                {
                    BusinessProfileId = user.UserBusinessProfiles.FirstOrDefault().BusinessProfileId,
                    EmailTemplateMapTypeId = EmailTemplateType.AccountPasswordChangeTemplate,
                    EmailTypeId = EmailType.SingleEmail,
                    ExecutionTime = DateTime.UtcNow,
                    NoOfAttempt = 3
                };

                email.EmailRecipients.Add(new EmailRecipientTo { Email = user.Email, Name = user.Contact.DisplayName });

                email.EmailMappingObject.Add(new EmailMappingModel { key = "RecipientName", Value = user.Contact.DisplayName });
                email.EmailMappingObject.Add(new EmailMappingModel { key = "PasswordRestLink", Value = callbackUrl });
                email.EmailMappingObject.Add(new EmailMappingModel { key = "ContactUsLink", Value = "#" }); //TODO: Need to update contact us link
                email.EmailMappingObject.Add(new EmailMappingModel { key = "RecipientEmail", Value = user.Email });
                email.EmailMappingObject.Add(new EmailMappingModel { key = "ProcessDate", Value = DateTime.UtcNow.ToLocalDateTime(user.Contact.TimeZoneId, DateTimeKind.Local).ToLongDateString() });

                this.emailJobQueueService.AddEmailToJobQueueUsingAppService(email);

                return View("ForgotPasswordConfirmation");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpGet]
        public IActionResult ForgotPasswordConfirmation(string returnUrl)
        {
            ForgotPasswordViewModel vm = new ForgotPasswordViewModel
            {
                ReturnUrl = returnUrl
            };
            return View(vm);
        }

        [HttpGet]
        public IActionResult ResetPassword(string token = null)
        {

            if (string.IsNullOrWhiteSpace(token))
            { return View("Error"); }
            ResetPasswordViewModel vm = new ResetPasswordViewModel
            {
                Token = token
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (string.IsNullOrWhiteSpace(model.Token))
            {
                return View();
            }

            var token = Encryption.Decrypt(model.Token, true);

            ResetPasswordToken resetPasswordToken = JsonConvert.DeserializeObject<ResetPasswordToken>(token);

            //var user = await userManager.FindByIdAsync(resetPasswordToken.UserId);

            ApplicationUser user = userManager.Users.Where(u => u.Id == resetPasswordToken.UserId)
                    .Include(u => u.Contact).FirstOrDefault();
                    

            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation");
            }

            Dictionary<string, string> passworderrors = await securityProfileService.CheckPasswordAsync(user.SecurityProfileId.Value, model.Password, user.UserName, user.Contact.DisplayName);

            if (passworderrors.Any())
            {
                foreach (var error in passworderrors)
                {
                    ModelState.AddModelError(error.Key, error.Value);
                }
                return View();
            }

            var result = await userManager.ResetPasswordAsync(user, resetPasswordToken.Code, model.Password);
            if (result.Succeeded)
            {
                IdentityServerConfig identityServerConfig = ItmConfigurations.IdentityServerConfigs.Where(x => x.ClientId == resetPasswordToken.ClientId).FirstOrDefault();
                string redirectUrl = identityServerConfig.RedirectUris.First();

                return Redirect(redirectUrl);
            }

            if (result.Errors.Any())
            {
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult ResetPasswordConfirmation(string token = null)
        {
            return View();
        }

        [HttpGet]
        public IActionResult BusinessProfile(string token, string returnUrl )
        {
            LoginBusinessProfileViewModel model = new LoginBusinessProfileViewModel();
            model.Token = token;
            model.ReturnUrl = returnUrl;

            var userId = Encryption.Decrypt(token);

            ApplicationUser user = userManager.Users.Where(u => u.Id == userId)
                    .Include(u => u.UserBusinessProfiles).ThenInclude(x => x.BusinessProfile).FirstOrDefault();

            model.BusinussProfileOptions = user.UserBusinessProfiles.Select(x => new SelectListItem { Value = x.BusinessProfile.Id.ToString(), Text = x.BusinessProfile.CompanyName }).ToList();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BusinessProfile(LoginBusinessProfileViewModel model, string returnUrl)
        {

            var userId = Encryption.Decrypt(model.Token, true);


            ApplicationUser user = await userManager.FindByIdAsync(userId);

            var userClaims = await userManager.GetClaimsAsync(user);

            await userManager.RemoveClaimsAsync(user, userClaims);

            await userManager.AddClaimAsync(user, new Claim(ApplicationConstants.BusinessProfileIdClaim, model.BusinussProfileId));

            if (!user.LockoutEnabled)
            {
                await events.RaiseAsync(new UserLoginFailureEvent(user.UserName, AccountOptions.LoginInvalidAccountLockedForever));
            }

            await events.RaiseAsync(new UserLoginSuccessEvent(user.UserName, user.Id.ToString(), user.UserName));

            // make sure the returnUrl is still valid, and if so redirect back to authorize endpoint or a local page
            // the IsLocalUrl check is only necessary if you want to support additional local pages, otherwise IsValidReturnUrl is more strict
            if (interaction.IsValidReturnUrl(model.ReturnUrl) || Url.IsLocalUrl(model.ReturnUrl))
            {
                return Redirect(model.ReturnUrl);
            }

            return Redirect("~/");
        }
    }

    public class LoginBusinessProfileViewModel
    {
        public string Token { get; set; }
        public string ReturnUrl { get; set; }
        public string BusinussProfileId { get; set; }
        public List<SelectListItem> BusinussProfileOptions { get; set; }
    }
}