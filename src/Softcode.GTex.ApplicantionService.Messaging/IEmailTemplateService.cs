using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using Softcode.GTex.ApplicantionService.Messaging.Models;
using Softcode.GTex.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Softcode.GTex.ApplicantionService.Messaging
{
    public interface IEmailTemplateService
    {
        #region Get List
        Task<LoadResult> GetEmailTemplateListAsync(DataSourceLoadOptionsBase options);
        #endregion

        #region Get Single Entity
        Task<EmailTemplate> GetEmailTemplateByIdAsync(int id);
        #endregion

        #region CUD
        Task<int> SaveEmailTemplateAsync(int id, EmailTemplateModel model);
        Task<int> DeleteEmailTemplateAsync(int id);
        Task<bool> DeleteEmailTemplatesAsync(List<int> ids);
        #endregion
    }
}
