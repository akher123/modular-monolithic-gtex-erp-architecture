using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;
using Softcode.GTex.Data;
using Softcode.GTex.Data.Models;

namespace Softcode.GTex.ApploicationService
{
    //public class CustomPasswordValidator<TUser> : PasswordValidator<TUser> where TUser : IdentityUser<string>
    public class CustomPasswordValidator : IPasswordValidator<ApplicationUser>
    {
        private IRepository<SecurityProfile> securityProfileRepository;
        public string ErrorMessage = "";

        public IRepository<SecurityProfile> SecurityProfileRepository
        {
            set { this.securityProfileRepository = value; }
        }

        public Task<IdentityResult> ValidateAsync(UserManager<ApplicationUser> manager, ApplicationUser user, string password)
        {

            if (string.IsNullOrEmpty(password))
            {
                return Task.FromResult(IdentityResult.Success);
            }

            //check whether the security policy validation is correct or not
            if (this.ValidatePassword(password, user.SecurityProfileId.Value))
            {
                return Task.FromResult(IdentityResult.Success);
            }

            return Task.FromResult(IdentityResult.Failed(new IdentityError
            {
                Description = "Security policy error"
            }));

        }

        /// <summary>
        /// Method to validate password.
        /// </summary>
        /// <param name="password"></param>
        /// <param name="userID"></param>
        /// <param name="profileId"></param>
        /// <param name="userFullName"></param>
        /// <param name="userName"></param>
        /// <param name="editMode"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        /// public bool ValidatePassword(string password, long userID, long profileID, string userFullName, string userName, int editMode, ref string errorMessage)
        public bool ValidatePassword(string password, int profileId)
        {
            bool isOk = false;
            SecurityProfile profileConfig = null;

            if (this.securityProfileRepository == null)
                return true;

            if (profileId > 0)
            {
                profileConfig = this.securityProfileRepository.Where(t => t.Id == profileId).FirstOrDefault();
            }

            int noOfLowerCaseCharsRequire = profileConfig.MinLowerCaseCharacter;
            int noOfUpperCaseCharsRequire = profileConfig.MinUpperCaseCharacter;
            int noOfDigitsRequire = profileConfig.MinDigit;
            int noOfSpecialCharsRequire = profileConfig.MinSpecialCharacter;

            bool isValidLowerCaseCharsRequire = false;
            bool isValidUpperCaseCharsRequire = false;
            bool isValidDigitsRequire = false;
            bool isValidSpecialCharsRequire = false;
            bool isValidMinimumPasswordChars = false;
            bool isValidMaximumPasswordChars = false;



            int digitCount = Regex.Matches(password, @"\d").Count;
            int upperCount = Regex.Matches(password, @"[A-Z]").Count;
            int lowerCount = Regex.Matches(password, @"[a-z]").Count;
            int specialCount = Regex.Matches(password, @"[~@#!""£$%^&*())]").Count;

            if (profileConfig.RequireLowerCaseCharacter)
            {
                if (noOfLowerCaseCharsRequire <= lowerCount)
                    isValidLowerCaseCharsRequire = true;
            }
            else
            {
                isValidLowerCaseCharsRequire = true;
            }

            if (profileConfig.RequireUpperCaseCharacter)
            {
                if (noOfUpperCaseCharsRequire <= upperCount)
                    isValidUpperCaseCharsRequire = true;
            }
            else
            {
                isValidUpperCaseCharsRequire = true;
            }

            if (profileConfig.RequireDigit)
            {
                if (noOfDigitsRequire <= digitCount)
                    isValidDigitsRequire = true;
            }
            else
            {
                isValidDigitsRequire = true;
            }

            if (profileConfig.RequireSpecialCharacter)
            {
                if (noOfSpecialCharsRequire <= specialCount)
                    isValidSpecialCharsRequire = true;
            }
            else
            {
                isValidSpecialCharsRequire = true;
            }

            if (profileConfig.MinPasswordLength <= password.Length)
                isValidMinimumPasswordChars = true;
            if (profileConfig.MaxPasswordLength >= password.Length)
                isValidMaximumPasswordChars = true;




            if (!isValidMinimumPasswordChars || !isValidMaximumPasswordChars)
            {
                //ErrorMessage += string.Format(Itm.Resource.Message.Security.USER_INVALID_PASSWORD_LENGTH, profileConfig.MinPasswordLength, profileConfig.MaxPasswordLength);
                ErrorMessage += string.Format("Password length must be in between {0} and {1} characters.", profileConfig.MinPasswordLength, profileConfig.MaxPasswordLength);
            }
            if (!isValidLowerCaseCharsRequire)
            {
                if (ErrorMessage.Length > 0) ErrorMessage += "</br>";
                //ErrorMessage += string.Format(Itm.Resource.Message.Security.USER_INVALID_PASSWORD_LOWERCASE, noOfLowerCaseCharsRequire);
                ErrorMessage += string.Format("Password require minimum {0} lower-case character(s).", noOfLowerCaseCharsRequire);
            }
            if (!isValidUpperCaseCharsRequire)
            {
                if (ErrorMessage.Length > 0) ErrorMessage += "</br>";
                //ErrorMessage += string.Format(Itm.Resource.Message.Security.USER_INVALID_PASSWORD_UPPERCASE, noOfUpperCaseCharsRequire);
                ErrorMessage += string.Format("Password require minimum {0} upper-case character(s).", noOfUpperCaseCharsRequire);
            }
            if (!isValidDigitsRequire)
            {
                if (ErrorMessage.Length > 0) ErrorMessage += "</br>";
                //ErrorMessage += string.Format(Itm.Resource.Message.Security.USER_INVALID_PASSWORD_DIGIT, noOfDigitsRequire);
                ErrorMessage += string.Format("Password require minimum {0} digit(s).", noOfDigitsRequire);
            }
            if (!isValidSpecialCharsRequire)
            {
                if (ErrorMessage.Length > 0) ErrorMessage += "</br>";
                //ErrorMessage += string.Format(Itm.Resource.Message.Security.USER_INVALID_PASSWORD_SPEC_CHARECTER, noOfSpecialCharsRequire);
                ErrorMessage += string.Format("Password require minimum {0} special character(s).", noOfSpecialCharsRequire);
            }


            isOk = isValidMinimumPasswordChars && isValidMaximumPasswordChars && isValidLowerCaseCharsRequire && isValidUpperCaseCharsRequire && isValidDigitsRequire && isValidSpecialCharsRequire;

            return isOk;
        }
    }
}
