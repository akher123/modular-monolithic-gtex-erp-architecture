using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using Softcode.GTex.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Softcode.GTex.ApplicantionService.Messaging
{
    public interface IEmailQueueService
    {
        Task<LoadResult> GetEmailQueueListAsync(DataSourceLoadOptionsBase options);

        void SendQueuedEmail(int pageSize=10);

        Task<bool> SendEmailAsync(int id);
    }
}
