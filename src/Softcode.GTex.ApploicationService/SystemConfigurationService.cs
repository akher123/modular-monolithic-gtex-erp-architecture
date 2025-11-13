using Softcode.GTex.ApploicationService.Models;
using Softcode.GTex.Data;
using Softcode.GTex.Data.Models;
using Softcode.GTex.ExceptionHelper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Softcode.GTex.ApploicationService
{
    public class SystemConfigurationService : BaseService<SystemConfigurationService>, ISystemConfigurationService
    {        
        private readonly IRepository<SecurityConfiguration> securityConfigurationRepository;

        public SystemConfigurationService(IApplicationServiceBuilder applicationServiceBuilder
            , IRepository<SecurityConfiguration> securityConfigurationRepository) : base(applicationServiceBuilder)
        {            
            this.securityConfigurationRepository = securityConfigurationRepository;
        }
        /// <summary>
        /// get first or default security configurations
        /// </summary>
        /// <returns></returns>
        public async Task<SecurityConfigurationModel> GetSecurityConfigurationModelAsync()
        {
            SecurityConfiguration configuration = await securityConfigurationRepository.FindOneAsync(t => t.Id >0 );

            if (configuration == null)
            {
                throw new SoftcodeNotFoundException("Security configuration not found");
            }

            return Mapper.Map<SecurityConfigurationModel>(configuration);
        }
        /// <summary>
        /// update security configuration 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> SetSecurityConfigurationValuesAsync(SecurityConfigurationModel model)
        {
            if (model == null)
            {
                throw new SoftcodeArgumentMissingException("Invalid category data");
            }

            SecurityConfiguration configurationObj = securityConfigurationRepository.FindOne(x => x.Id >0);

            if (configurationObj != null)
            {
                configurationObj.MaximumImageUploadSizeInKb = model.MaximumImageUploadSizeInKB;
                configurationObj.MaximumSesssionSpaceInKb = model.MaximumSesssionSpaceInKB;
                configurationObj.DefaultUserAuthType = model.DefaultUserAuthType;
                configurationObj.B2busernameType = model.B2BUsernameType;
                configurationObj.EmployeeUsernameType = model.EmployeeUsernameType;
                configurationObj.OtherUsernameType = model.OtherUsernameType;
                configurationObj.EnableDataAuditLog = model.EnableDataAuditLog;
                configurationObj.EnableErrorLog = model.EnableErrorLog;
                configurationObj.EnableSessionLog = model.EnableSessionLog;
                configurationObj.EnableAuthenticationLog = model.EnableAuthenticationLog;
                configurationObj.EnableAccessibilityCompliant = model.EnableAccessibilityCompliant;
                configurationObj.EnableAutoLogin = model.EnableAutoLogin;
                configurationObj.EnableRetrievePassword = model.EnableRetrievePassword;
                configurationObj.EnableCaptcha = model.EnableCaptcha;
                configurationObj.EnablePasswordResetRestriction = model.EnablePasswordResetRestriction;
                configurationObj.NumberOfAttemptsBeforeCaptcha = model.NumberOfAttemptsBeforeCaptcha;
                configurationObj.EnableSso = model.EnableSSO;
                configurationObj.DeletePhysicalDocument = model.DeletePhysicalDocument;
                configurationObj.MustAcceptTerms = model.MustAcceptTerms;
                configurationObj.ApplicationTitle = model.ApplicationTitle;
                configurationObj.AppHelpContentUrl = model.AppHelpContentURL;
                configurationObj.OtherSettings = model.OtherSettings;                                
                
                if (model.Id == 0)
                {
                    await securityConfigurationRepository.CreateAsync(configurationObj);
                }
                else
                {
                    await securityConfigurationRepository.UpdateAsync(configurationObj);
                }
            }
            return configurationObj.Id;
        }
        /// <summary>
        /// get user authentication type
        /// </summary>
        /// <returns></returns>
        public List<SelectModel> GetUserAuthenticationTypeSelectItems()
        {            
            return Utilities.GetEnumValueList(typeof(Configuration.Enums.UserAuthenticationType));
        }
        /// <summary>
        /// get user name type
        /// </summary>
        /// <returns></returns>
        public List<SelectModel> GetUserNameTypeSelectedItem()
        {
            return Utilities.GetEnumValueList(typeof(Configuration.Enums.UsernameType));
        }
    }
}
