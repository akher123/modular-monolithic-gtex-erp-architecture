using Softcode.GTex.ApplicantionService.Messaging.Models;
using Softcode.GTex.Data.Models;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Softcode.GTex.ApplicantionService.Messaging
{
    public class GenericEmailProcessor : IEmailProcessor
    {
        public void PrepareEmailContentAsync(EmailJobQueue emailJobQueue, EmailQueue emailQueue)
        {
            
            MemoryStream memorystreamd = new MemoryStream(emailJobQueue.MappingObject);
            BinaryFormatter binaryFormatter = new BinaryFormatter();

            emailQueue.Body = MailService.GetFormattedHtmlContentWithPublicContentUrl(emailJobQueue.LayoutContent);
            emailQueue.Subject = emailJobQueue.Subject;

            if (!(binaryFormatter.Deserialize(memorystreamd) is List<EmailMappingModel> mappingObject))
            {
                return;
            }

            foreach (EmailMappingModel item in mappingObject)
            {
                emailQueue.Body = emailQueue.Body.Replace("{{" + item.key + "}}", item.Value);
                emailQueue.Subject = emailQueue.Subject.Replace("{{" + item.key + "}}", item.Value);
            }
        }
    }
}
