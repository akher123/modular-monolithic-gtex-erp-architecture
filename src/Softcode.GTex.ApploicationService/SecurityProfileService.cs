using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using Softcode.GTex.ApploicationService.Models;
using Softcode.GTex.Configuration;
using Softcode.GTex.Data;
using Softcode.GTex.Data.Models;
using Softcode.GTex.ExceptionHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Softcode.GTex.ApploicationService
{
    public class SecurityProfileService : BaseService<SecurityProfileService>, ISecurityProfileService
    {
        private readonly IRepository<SecurityProfile> securityProfileRepository;

        public SecurityProfileService(IApplicationServiceBuilder applicationServiceBuilder
            , IRepository<SecurityProfile> securityProfileRepository)
            : base(applicationServiceBuilder)
        {
            this.securityProfileRepository = securityProfileRepository;
        }

        public async Task<LoadResult> GetSecurityPrifileListAsync(DataSourceLoadOptionsBase options)
        {
            options.Select = new[] { "Id", "ProfileName", "Descriptions", "IsDefault" };
            return await securityProfileRepository.GetDevExpressListAsync(options);
        }
        /// <summary>
        /// get security profile selected item
        /// </summary>
        /// <returns></returns>
        public async Task<List<SelectModel>> GetSecurityProfileSelectItemsAsync()
        {
            return await Task.Run(()=> securityProfileRepository.Where(x=>x.IsActive)
                            .Select(x => new SelectModel {
                                Id = x.Id,
                                Name = x.ProfileName,
                                IsDefault = x.IsDefault
                            }).ToList());            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<SecurityProfileModel> GetSecurityProfileByIdAsync(int id)
        {
            if (id == 0)
            {
                return new SecurityProfileModel();
            }
            SecurityProfile securityProfile = await securityProfileRepository.FindOneAsync(x => x.Id == id);
            if (securityProfile == null)
            {
                throw new SoftcodeNotFoundException("Security Profile not found");
            }
            return Mapper.Map<SecurityProfileModel>(securityProfile);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> SaveSecurityProfileDetailsAsync(int id, SecurityProfileModel model)
        {
            if (model == null)
            {
                throw new SoftcodeArgumentMissingException("Invalid Security Profile data");
            }
            SecurityProfile dbSP = new SecurityProfile();
            if (id > 0)
            {
                dbSP = await securityProfileRepository.FindOneAsync(x => x.Id == id);

                if (dbSP == null)
                {
                    throw new SoftcodeNotFoundException("Security Profile not found for edit");
                }
            }
            else
            {
                dbSP.BusinessProfileId = LoggedInUser.DefaultBusinessProfileId;
            }

            Mapper.Map(model, dbSP);

            if (!dbSP.EnableAccountLockout)
            {
                dbSP.NumberOfAttemptsBeforeLockout = 0;
                dbSP.LockoutDuration = 0;
            }
            dbSP.IsActive = true;
            //dbSP.TimeStamp = new byte[8];

            if (model.IsDefault)
            {
                //set default = false for previous default record 
                securityProfileRepository.Attach(securityProfileRepository
                                                .Where(x => x.Id != model.Id && x.IsDefault).ToList()
                                                .Select(x => { x.IsDefault = false; return x; }).FirstOrDefault());
            }


            if (id > 0)
            {
                 await securityProfileRepository.UpdateAsync(dbSP);
            }
            else
            {
                await securityProfileRepository.CreateAsync(dbSP);
            }
            return dbSP.Id;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<int> DeleteSecurityProfileAsync(int id)
        {
            SecurityProfile securityProfile = await securityProfileRepository.FindOneAsync(x => x.Id == id);
            if (securityProfile == null)
            {
                throw new SoftcodeNotFoundException("Security Profile not found");
            }
            if (securityProfile.IsDefault)
            {
                throw new SoftcodeInvalidDataException("Default security profile cannot be deleted.");
            }

            return await securityProfileRepository.DeleteAsync(securityProfile);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<bool> DeleteSecurityProfilesAsync(List<int> ids)
        {
            if (ids == null)
            {
                throw new SoftcodeArgumentMissingException("Security Profile not found");
            }
            if (!await securityProfileRepository.ExistsAsync(x => ids.Contains(x.Id)))
            {
                throw new SoftcodeArgumentMissingException("Security Profile not found");
            }

            if (await securityProfileRepository.ExistsAsync(x => ids.Contains(x.Id) && x.IsDefault))
            {
                throw new SoftcodeInvalidDataException("Default security profile cannot be deleted.");
            }

            return await securityProfileRepository.DeleteAsync(t => ids.Contains(t.Id)) > 0;
        }

        public async Task<Dictionary<string, string>> CheckPasswordAsync(int securityProfileId, string password, string userName, string userFullName)
        {
            Dictionary<string, string> errors = new Dictionary<string, string>();

            if (string.IsNullOrWhiteSpace(password))
            {
                errors.Add("requried", "Password cannot be null");

                return errors;
            }

                SecurityProfile securityProfile = await securityProfileRepository.FindOneAsync(x => x.Id == securityProfileId);

            if(securityProfile == null)
            {
                securityProfile = await securityProfileRepository.FindOneAsync(x => x.IsDefault);
            }

            if (securityProfile == null)
            {
                throw new SoftcodeNotFoundException("Invalid security profile");
            }


            if(password.Length < securityProfile.MinPasswordLength)
            {
                errors.Add("minPasswordLength", $"Password length must be in between {securityProfile.MinPasswordLength} and {securityProfile.MaxPasswordLength} characters.");
            }

            if (password.Length > securityProfile.MaxPasswordLength)
            {
                errors.Add("minPasswordLength", $"Password length must be in between {securityProfile.MinPasswordLength} and {securityProfile.MaxPasswordLength} characters.");
            }

            int numberOfLowerCase = password.GetNumberOfLowercase();
            int numberOfUpperCase = password.GetNumberOfUppercase();
            int numberOfDigit = password.GetNumberOfDigit();
            int numberOfSpecialCharacter = password.GetNumberOfSpecialCharacter();

            switch(securityProfile.PasswordCombinationTypeId)
            {
                case PasswordCombinationType.Any:
                    break;
                case PasswordCombinationType.AtLeast2Types:
                    if(securityProfile.MinLowerCaseCharacter > password.GetNumberOfLowercase() || 
                        securityProfile.MinUpperCaseCharacter > password.GetNumberOfUppercase())
                    {
                        errors.Add("passwordCharacterCriteria", $"Password must be contains at least '{securityProfile.MinLowerCaseCharacter}' lower case and '{securityProfile.MinUpperCaseCharacter}' upper case characters.");
                    }
                    break;
                case PasswordCombinationType.AtLeast3Types:
                    if (securityProfile.MinLowerCaseCharacter > password.GetNumberOfLowercase() || 
                        securityProfile.MinUpperCaseCharacter > password.GetNumberOfUppercase() || 
                        securityProfile.MinDigit > password.GetNumberOfDigit())
                    {
                        errors.Add("passwordCharacterCriteria", $"Password must be contains at least '{securityProfile.MinLowerCaseCharacter}' lower case, '{securityProfile.MinUpperCaseCharacter}' upper case and '{securityProfile.MinDigit}' digit characters.");
                    }
                    break;
                case PasswordCombinationType.All4Types:
                    if (securityProfile.MinLowerCaseCharacter > password.GetNumberOfLowercase() || 
                        securityProfile.MinUpperCaseCharacter > password.GetNumberOfUppercase() || 
                        securityProfile.MinDigit > password.GetNumberOfDigit() || 
                        securityProfile.MinSpecialCharacter > password.GetNumberOfSpecialCharacter())
                    {
                        errors.Add("passwordCharacterCriteria", $"Password must be contains at least '{securityProfile.MinLowerCaseCharacter}' lower case, '{securityProfile.MinUpperCaseCharacter}' upper case, '{securityProfile.MinDigit}' digit  and '{securityProfile.MinSpecialCharacter}' special characters.");
                    }
                    break;
                default:
                    break;

            }

            if(securityProfile.DisallowUserNameInPassword)
            {
                if(password.Contains(userName))
                {
                    errors.Add("disallowUserNameInPassword", $"Password must not contain user information");
                }
            }

            if (securityProfile.DisallowPartsOfNameInPassword)
            {
                string[] usernames = userFullName.Split(new string[] {" "}, StringSplitOptions.RemoveEmptyEntries);

                if (usernames.Contains(password))
                {
                    errors.Add("disallowPartsOfNameInPassword", $"Password must not contain user information");
                }
            }

            return errors;
        }
    }
}
