using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using Softcode.GTex.ApplicantionService.Messaging.Models;
using System.Threading.Tasks;

namespace Softcode.GTex.ApplicantionService.Messaging
{
    public interface IServerSettingService
    {
        Task<LoadResult> GetServerSettingListAsync(DataSourceLoadOptionsBase options);

        Task<EmailServerModel> GetServerSettingAsync(int id);
        Task<int> SaveServerSettingAsync(int id, EmailServerModel model);
        Task<bool> DeleteEmailServerAsync(int id);

        bool SendTestEmail(EmailServerModel model);
    }
}
