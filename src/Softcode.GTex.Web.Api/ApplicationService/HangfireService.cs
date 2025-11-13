using Hangfire;
using Softcode.GTex.ApplicantionService.Messaging;
using System;

namespace Softcode.GTex.Web.Api.ApplicationService
{
    public class HangfireService : IHangfireService
    {
        private readonly IEmailJobQueueService emailJobQueueService;
        private readonly IEmailQueueService emailQueueService;
        public HangfireService(IEmailJobQueueService emailJobQueueService, IEmailQueueService emailQueueService)
        {
            this.emailJobQueueService = emailJobQueueService;
            this.emailQueueService = emailQueueService;
        }

        public void StartEmailJobQueueService()
        {
            emailJobQueueService.ProcessQueuedMessagesAsync();

        }

        public void StartEmailQueueService()
        {
            emailQueueService.SendQueuedEmail();
        }
    }

    public interface IHangfireService
    {
        void StartEmailJobQueueService();

        void StartEmailQueueService();
    }


    public class HangfireActivator : JobActivator
    {
        private readonly IServiceProvider _serviceProvider;

        public HangfireActivator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public override object ActivateJob(Type type)
        {
            return _serviceProvider.GetService(type);
        }
    }
}
