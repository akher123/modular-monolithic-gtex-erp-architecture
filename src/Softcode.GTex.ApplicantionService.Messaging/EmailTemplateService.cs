using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using Softcode.GTex.ApplicantionService.Messaging.Models;
using Softcode.GTex.ApploicationService;
using Softcode.GTex.Data.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Softcode.GTex.ApplicantionService.Messaging
{
    public class EmailTemplateService : BaseService<EmailTemplateService>, IEmailTemplateService
    {
        public EmailTemplateService(IApplicationServiceBuilder applicationServiceBuilder
            ) : base(applicationServiceBuilder)
        {
        }

        public Task<int> DeleteEmailTemplateAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteEmailTemplatesAsync(List<int> ids)
        {
            throw new NotImplementedException();
        }

        public Task<EmailTemplate> GetEmailTemplateByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<LoadResult> GetEmailTemplateListAsync(DataSourceLoadOptionsBase options)
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveEmailTemplateAsync(int id, EmailTemplateModel model)
        {
            throw new NotImplementedException();
        }

        
    }
}
