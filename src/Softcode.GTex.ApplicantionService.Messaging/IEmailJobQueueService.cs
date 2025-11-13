using Softcode.GTex.ApplicantionService.Messaging.Models;
using System.Threading.Tasks;

namespace Softcode.GTex.ApplicantionService.Messaging
{
    public interface IEmailJobQueueService
    {
        void ProcessQueuedMessagesAsync(int pageSize = 100);
        bool AddEmailToJobQueue(EmailJobQueueModel emailJobQueue);
        bool AddEmailToJobQueueUsingAppService(EmailJobQueueModel emailJobQueue);
        
    }
}
