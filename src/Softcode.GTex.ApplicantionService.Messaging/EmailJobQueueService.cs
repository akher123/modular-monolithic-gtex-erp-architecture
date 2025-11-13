using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Softcode.GTex.ApplicantionService.Messaging.Models;
using Softcode.GTex.ApploicationService;
using Softcode.GTex.Configuration;
using Softcode.GTex.Data;
using Softcode.GTex.Data.Models;
using Softcode.GTex.ExceptionHelper;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace Softcode.GTex.ApplicantionService.Messaging
{
    public class EmailJobQueueService : BaseService<EmailJobQueueService>, IEmailJobQueueService
    {

        private readonly IRepository<EmailJobQueue> emailJobQueueRepository;
        private readonly IRepository<EmailTemplate> emailTemplateRepository;

        public EmailJobQueueService(IApplicationServiceBuilder applicationServiceBuilder
            , IRepository<EmailJobQueue> emailJobQueueRepository
            , IRepository<EmailTemplate> emailTemplateRepository
            ) 
            : base(applicationServiceBuilder)
        {
            this.emailJobQueueRepository = emailJobQueueRepository;
            this.emailTemplateRepository = emailTemplateRepository;
        }

       

        public bool AddEmailToJobQueueUsingAppService(EmailJobQueueModel model)
        {
            emailJobQueueRepository.IsApplicationServiceUser = true;
            return AddEmailToJobQueue(model);
        }

        public bool AddEmailToJobQueue(EmailJobQueueModel model)
        {

            emailJobQueueRepository.IsApplicationServiceUser = true;

            if (model.EmailRecipients.Count == 0 || (model.EmailTemplateId == 00 && model.EmailTemplateMapTypeId == 0))
            {
                throw new SoftcodeInvalidDataException("Invalid Job Queue object.");
            }

            EmailTemplate template = model.EmailTemplateId > 0 ? emailTemplateRepository.Where(t => t.Id == model.EmailTemplateId).Include(t => t.EmailServer).FirstOrDefault() 
                : emailTemplateRepository.Where(t => t.BusinessMapTypeId == model.EmailTemplateMapTypeId).Include(t => t.EmailServer).FirstOrDefault();

            EmailJobQueue emailJobQueue = new EmailJobQueue
            {
                BusinessProfileId = model.BusinessProfileId,
                EmailTypeId = model.EmailTypeId,
                EntityId = model.EntityId,
                EntityTypeId = model.EntityTypeId,
                ExecutionTime = model.ExecutionTime,
                NoOfAttempt = model.NoOfAttempt,
                Subject = model.Subject,
                LayoutContent = model.LayoutContent,
                SenderName = model.SenderName,
                SenderEmail = model.SenderEmail,
                StatusId = EmailQueueStatus.Pending,
                IsActive = true
            };
            //emailJobQueue.EmailTemplateId = template.Id;


            if (template != null)
            {
                emailJobQueue.EmailTemplateId = template.Id;

                if (string.IsNullOrWhiteSpace(emailJobQueue.LayoutContent))
                {
                    emailJobQueue.LayoutContent = template.LayoutContent;
                }

                if (string.IsNullOrWhiteSpace(emailJobQueue.Subject))
                {
                    emailJobQueue.Subject = template.Subject;
                }

                if (string.IsNullOrWhiteSpace(emailJobQueue.SenderName))
                {
                    emailJobQueue.SenderName = template.EmailServer.SenderName;
                }
                if (string.IsNullOrWhiteSpace(emailJobQueue.SenderEmail))
                {
                    emailJobQueue.SenderEmail = template.EmailServer.SenderId;
                }
            }

            emailJobQueue.EmailRecipients = model.EmailRecipients;


            MemoryStream memorystream = new MemoryStream();
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(memorystream, model.EmailMappingObject);
            emailJobQueue.MappingObject = memorystream.ToArray();

            //TODO: Need To add attachments 


            return emailJobQueueRepository.CreateAsync(emailJobQueue).Result.Id > 0;
        }

        public void ProcessQueuedMessagesAsync(int pageSize = 100)
        {
            Logger.LogError("Hangfire service called");
            emailJobQueueRepository.IsApplicationServiceUser = true;
            EmailJobQueue[] emailJob = emailJobQueueRepository
                            .Where(e => e.IsActive && !e.IsArchived && e.ExecutionTime <= DateTime.UtcNow
                                   && e.StatusId == EmailQueueStatus.Pending)
                            .Include(e => e.EmailAttachments).Include(e => e.EmailRecipients).Include(e => e.EmailTemplate).ThenInclude(t => t.BusinessMapType)
                            .OrderBy(e => e.EmailTemplate.Priority)
                            .Take(100).ToArray();

            Parallel.For(0, emailJob.Length,
                   index =>
                   {
                       PrepareEmailQueue(emailJob[index]);
                   });

        }

        private void PrepareEmailQueue(EmailJobQueue emailJobQueue)
        {
            try
            {
                if (emailJobQueue.EmailTypeId != EmailType.SingleEmail)
                {
                    foreach (EmailRecipientTo to in emailJobQueue.EmailRecipients.OfType<EmailRecipientTo>())
                    {
                        EmailQueue email = PrepareEmailObject(emailJobQueue);
                        email.RecipientEmail = to.Email;
                        email.RecipientName = to.Name;
                    }
                }
                else
                {
                    PrepareEmailObject(emailJobQueue);
                }
            }
            catch (Exception ex)
            {
                emailJobQueue.StatusId = EmailQueueStatus.Failed;
                emailJobQueue.ErrorText = ex.Message;
                Logger.LogError(ex.Message, ex);
            }

            emailJobQueueRepository.Update(emailJobQueue);
        }

        private EmailQueue PrepareEmailObject(EmailJobQueue emailJobQueue)
        {
            EmailQueue emailQueue = new EmailQueue
            {
                EmailJobQueueId = emailJobQueue.Id,
                StatusId = EmailQueueStatus.Pending,
                Priority = emailJobQueue.EmailTemplate.Priority,
                IsArchived = false,
                Subject = emailJobQueue.Subject,
                Body = emailJobQueue.LayoutContent
            };

            // get type of class Calculator from just loaded assembly
            Type emailProcessorType = Assembly.GetExecutingAssembly().GetType("Softcode.GTex.ApplicantionService.Messaging." + emailJobQueue.EmailTemplate.BusinessMapType.ActionKey);

            // create instance of class Calculator
            IEmailProcessor emailProcessor = Activator.CreateInstance(emailProcessorType) as IEmailProcessor;

            //var emailProcessor = (IEmailProcessor)Activator.CreateInstance("Softcode.GTex.ApplicantionService.Messaging", "Softcode.GTex.ApplicantionService.Messaging." + emailJobQueue.EmailTemplate.BusinessMapType.ActionKey);
            emailProcessor?.PrepareEmailContentAsync(emailJobQueue, emailQueue);

            emailJobQueue.EmailQueues.Add(emailQueue);

            emailJobQueue.StatusId = EmailQueueStatus.InProgress;

            return emailQueue;
        }
    }
}
