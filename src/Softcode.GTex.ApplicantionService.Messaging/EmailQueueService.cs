using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using Microsoft.EntityFrameworkCore;
using Softcode.GTex.ApploicationService;
using Softcode.GTex.Configuration;
using Softcode.GTex.Data;
using Softcode.GTex.Data.Models;
using Softcode.GTex.ExceptionHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Softcode.GTex.ApplicantionService.Messaging
{
    public class EmailQueueService : BaseService<EmailQueueService>, IEmailQueueService
    {
        private readonly IRepository<EmailQueue> emailQueueRepository;

        public EmailQueueService(IApplicationServiceBuilder applicationServiceBuilder
            , IRepository<EmailQueue> emailQueueRepository
            ) : base(applicationServiceBuilder)
        {
            this.emailQueueRepository = emailQueueRepository;
        }

        public Task<LoadResult> GetEmailQueueListAsync(DataSourceLoadOptionsBase options)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SendEmailAsync(int id)
        {
            EmailQueue email = await Task.Run(() => emailQueueRepository.Where(e => e.Id == id)
                    .Include(q => q.EmailJobQueue).ThenInclude(q => q.EmailTemplate).ThenInclude(q => q.EmailServer)
                    .Include(q => q.EmailJobQueue).ThenInclude(q => q.EmailRecipients).FirstOrDefault());
            if (email == null)
            {
                throw new SoftcodeNotFoundException("Email not found");
            }

            PrepareMessageAndSend(email);

            return true;
        }

        public void SendQueuedEmail(int pageSize = 10)
        {
            emailQueueRepository.IsApplicationServiceUser = true;

            List<EmailQueue> emailQueue = emailQueueRepository.Where(q => (q.StatusId == EmailQueueStatus.Pending || q.StatusId == EmailQueueStatus.Failed)
                        && q.FailedAttemptCount < q.EmailJobQueue.NoOfAttempt)
                    .Include(q => q.EmailJobQueue).ThenInclude(q => q.EmailTemplate).ThenInclude(q => q.EmailServer)
                    .Include(q => q.EmailJobQueue).ThenInclude(q => q.EmailRecipients)
                    .OrderBy(q => q.Priority).Take(pageSize).ToList();

            foreach (EmailQueue email in emailQueue)
            {
                PrepareMessageAndSend(email);
            }

            IsApplicationServiceUser = false;
        }

        private void PrepareMessageAndSend(EmailQueue email)
        {
            using (ItmSmtpClient smtp = MailService.GetMailServerSettings(email.EmailJobQueue))
            using (MailMessage message = MailService.GetMailMessage(email, smtp))
            {
                try
                {
                    email.EmailJobQueue.LastExecutedOn = DateTime.UtcNow;

                    if (!(message.To.Count > 0 && smtp.Port != 0))
                    {
                        throw new Exception("Invalid message object. Please check you have provided valid To, From, Host & Port");
                    }

                    smtp.Send(message);
                    email.StatusId = EmailQueueStatus.Sent;

                }
                catch (Exception ex)
                {
                    email.FailedAttemptCount++;
                    email.ErrorText = ex.Message;
                    email.StatusId = EmailQueueStatus.Failed;
                }

                if (email.StatusId == EmailQueueStatus.Sent)
                {
                    email.EmailJobQueue.StatusId = EmailQueueStatus.Finished;                    
                }
                else if (email.FailedAttemptCount == email.EmailJobQueue.NoOfAttempt)
                {
                    email.EmailJobQueue.StatusId = EmailQueueStatus.Failed;
                    email.ErrorText = $"Last {email.FailedAttemptCount} attempts to send email has failed.";
                }

                email.EmailJobQueue.LastExecutedOn = DateTime.UtcNow;

                emailQueueRepository.Update(email);
            }
        }
    }
}
