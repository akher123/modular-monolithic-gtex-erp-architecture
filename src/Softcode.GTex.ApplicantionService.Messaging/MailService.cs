using Softcode.GTex.ApplicantionService.Messaging.Models;
using Softcode.GTex.Configuration;
using Softcode.GTex.Data.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;

namespace Softcode.GTex.ApplicantionService.Messaging
{
    internal class MailService
    {
        #region variables, constructor & utility functions
        

        internal static void EmbedImages(string bodyContent, List<LinkedResource> resources, int findIndex = 0)
        {
            try
            {
                if (bodyContent == null || bodyContent.Count() <= findIndex) return;//check if start index is greater
                int startIndex = bodyContent.ToLower().IndexOf("<img ", findIndex);//find the image tag
                if (startIndex >= 0)
                {
                    int endIndex = bodyContent.IndexOf('>', startIndex + 5);//get end tag
                    if (endIndex >= 0)//check end tag
                    {
                        startIndex += 5;
                        endIndex -= 1;
                        string imgTags = bodyContent.Substring(startIndex, endIndex - startIndex);//get img tag content
                        if (imgTags != null && !imgTags.ToLower().Contains("cid:"))//check if null or already replaced
                        {
                            var tagArray = imgTags.ToLower().Trim().Split('"');//split the tag contents
                            if (tagArray == null || tagArray.Length <= 0) tagArray = imgTags.Split('\'');//if fails then split with '
                            if (tagArray != null)
                            {
                                var tagArraylist = tagArray.Select(t => t.Trim()).ToList();//make a list replacing all empty spaces
                                //tagArraylist = ;//(t=>t.StartsWith("src"));
                                var imageSourceIndex = tagArraylist.IndexOf("src=") + 1;//find src tag position
                                if (tagArraylist.Count > imageSourceIndex)//checking
                                {
                                    var imageUrl = tagArraylist[imageSourceIndex];//find the value of image url
                                    if (imageUrl != null)
                                    {
                                        var pathArray = imageUrl.Split('/');//split the url
                                        if (pathArray != null && pathArray.Length > 0)//checking
                                        {
                                            string imagePath = (string.IsNullOrEmpty(ApplicationEnvironment.PublicImageHostFolderPhysicalPath) ? ApplicationEnvironment.PublicImageStoragePhysicalPath : ApplicationEnvironment.PublicImageHostFolderPhysicalPath) + pathArray[pathArray.Length - 1];//construct image path
                                            if (File.Exists(imagePath))//check if file exists in server
                                            {
                                                int width = GetWidthHeightSizeValue(tagArraylist);//get width of the image
                                                int height = GetWidthHeightSizeValue(tagArraylist, false);//get height of the image
                                                string sizeProperty = "";
                                                if (width != 0 && height != 0)
                                                {
                                                    sizeProperty = String.Format(" width='{0}' height='{1}'", width, height);//construct size property
                                                }
                                                LinkedResource resource = new LinkedResource(imagePath, "image/jpeg");//add the image in resource
                                                var resourceID = Guid.NewGuid().ToString();//make a resource id
                                                resource.ContentId = resourceID;//set id
                                                bodyContent = bodyContent.Replace(imgTags, string.Format("src='cid:{0}'{1}", resourceID, sizeProperty));//replace img tab body with content
                                                resources.Add(resource);//expand the list

                                            }
                                        }
                                    }
                                }
                            }
                        }
                        EmbedImages(bodyContent, resources, endIndex + 2);//do it all over again
                    }
                }
            }
            catch (Exception ex) { throw new InvalidOperationException(ex.Message, ex); }

        }

        private static int GetWidthHeightSizeValue(List<string> tagArraylist, bool width = true)
        {
            int size = 0;
            try
            {
                string searchText = width ? "width" : "height";//check what need to be searched
                var tag = tagArraylist.FirstOrDefault(t => t.Trim().Contains(searchText + ":"));//see if tag exists
                bool isStyleTag = true;
                if (tag == null)
                {
                    tag = tagArraylist.FirstOrDefault(t => t.Trim().Contains(searchText + "="));//if doesn't exists then check with e.g., width=
                    isStyleTag = false;//if found then not part of style tag
                }
                if (tag != null)
                {
                    tag = tag.Replace(" ", null);
                    if (!isStyleTag && tagArraylist.Count > (tagArraylist.IndexOf(tag) + 1))//checking
                    {
                        size = tagArraylist[tagArraylist.IndexOf(tag) + 1].Trim().Replace("px", null).ToInt();//get the next value of width= replacing px and converting to int
                    }
                    else
                    {
                        var styleTagArray = tag.Split(';').Select(t => t.Trim()).ToList();//if style then split using ;
                        if (styleTagArray != null && styleTagArray.Count > 0)//checking
                        {
                            var findTag = styleTagArray.FirstOrDefault(t => t.Contains(searchText));//get the item
                            if (findTag != null)
                                return findTag.Replace(searchText + ":", null).Replace("px", null).ToInt();//replace e.g, width: and px and then send as int
                        }
                    }
                }
            }
            catch { return 0; }
            return size;
        }
        #endregion

       


        public static string GetFormattedHtmlContentWithPublicContentUrl(string bodyContent)
        {
            if (String.IsNullOrEmpty(bodyContent) || ApplicationEnvironment.DisableContentUrlMerge) return bodyContent;
            else
            {
                if (!string.IsNullOrEmpty(ApplicationEnvironment.AdminPortalDomainUrl))
                { 
                    var uri = new System.Uri(ApplicationEnvironment.AdminPortalDomainUrl);
                    var matchString = (uri.AbsolutePath.EndsWith("/") ? "" : uri.AbsolutePath) + ApplicationEnvironment.IMAGE_STORE_FOLDER_PUBLIC.Replace("~", "");
                    var replaceString = string.IsNullOrEmpty(ApplicationEnvironment.PublicImageHostUrl) ? (ApplicationEnvironment.AdminPortalDomainUrl + ApplicationEnvironment.IMAGE_STORE_FOLDER_PUBLIC.Replace("~", "")) : ApplicationEnvironment.PublicImageHostUrl;
                    bodyContent = bodyContent.Replace(matchString, replaceString);
                }
            }
             
            return bodyContent; 
        }


        internal static List<string> GetEmailAddresses(string addresses)
        {
            if (addresses == null) return new List<string>();
            addresses = addresses.Replace(',', ';');
            var list = new List<string>();
            var array = addresses.Split(';');
            foreach (var address in array)
            {
                if (!String.IsNullOrEmpty(address))
                {
                    list.Add(address.Trim());
                }
            }
            return list;
        }

        internal static ItmSmtpClient GetMailServerSettings(EmailJobQueue emailJobQueue)
        {
            ItmSmtpClient mailClient = new ItmSmtpClient();
            EmailServer mailserver = emailJobQueue.EmailTemplate.EmailServer;
            mailClient.MailServerId = mailserver.Id;
            if (mailserver.SenderOptionId == EmailServerSenderOption.Sendemailusingserverdetail)
            {
                mailClient.From = emailJobQueue.SenderEmail;
                mailClient.SenderName = emailJobQueue.SenderName;
            }
            else
            {
                mailClient.From = mailserver.SenderId;
                mailClient.SenderName = mailserver.SenderName;
            }
            mailClient.UseDefaultCredentials = false;
            mailClient.Credentials = new System.Net.NetworkCredential(mailserver.UserName, Encryption.Decrypt(mailserver.Password));
            mailClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            mailClient.EnableSsl = mailserver.UseSslforOutgoing;
            mailClient.Host = mailserver.OutgoingServer;
            mailClient.Port = mailserver.OutgoingPort == 0 ? 25 : mailserver.OutgoingPort;
            mailClient.ReplyToEmailAddress = mailserver.ReplyToEmailAddress;
            mailClient.CopyToEmailAddress = mailserver.CopyToEmailAddress;
            return mailClient;
        }

       

        internal static MailMessage GetMailMessage(EmailQueue email, ItmSmtpClient smtp)
        {
            MailMessage message = new MailMessage();
            
            message.From = new MailAddress(smtp.From, smtp.SenderName);
             
            if (string.IsNullOrWhiteSpace(email.RecipientEmail))
            {
                foreach (var recipient in email.EmailJobQueue.EmailRecipients.OfType<EmailRecipientTo>())
                {
                    if (string.IsNullOrEmpty(recipient.Name))
                    {
                        message.To.Add(recipient.Email);
                    }
                    else
                    {
                        message.To.Add(new MailAddress(recipient.Email, recipient.Name));
                    }
                }
            }
            else
            {
                if (string.IsNullOrEmpty(email.RecipientName))
                {
                    message.To.Add(email.RecipientEmail);
                }
                else
                {
                    message.To.Add(new MailAddress(email.RecipientEmail, email.RecipientName));
                }
            }



            foreach (var recipient in email.EmailJobQueue.EmailRecipients.OfType<EmailRecipientCc>())
            {
                if (string.IsNullOrEmpty(recipient.Name))
                {
                    message.CC.Add(recipient.Email);
                }
                else
                {
                    message.CC.Add(new MailAddress(recipient.Email, recipient.Name));
                }
            }

            foreach (var recipient in email.EmailJobQueue.EmailRecipients.OfType<EmailRecipientBcc>())
            {
                if (string.IsNullOrEmpty(recipient.Name))
                {
                    message.Bcc.Add(recipient.Email);
                }
                else
                {
                    message.Bcc.Add(new MailAddress(recipient.Email, recipient.Name));
                }
            }

            if (!String.IsNullOrEmpty(smtp.CopyToEmailAddress))
            {
                foreach (var address in MailService.GetEmailAddresses(smtp.CopyToEmailAddress))
                {
                    message.Bcc.Add(address);
                }
            }

            if (!String.IsNullOrEmpty(smtp.ReplyToEmailAddress))
            {
                foreach (var address in MailService.GetEmailAddresses(smtp.ReplyToEmailAddress))
                {
                    message.ReplyToList.Add(address);
                }
            }
            

            message.Attachments.Clear();
            foreach (var file in email.EmailJobQueue.EmailAttachments)
            {
                if (System.IO.File.Exists(file.FilePath))
                {
                    if (String.IsNullOrEmpty(file.FileName))
                    {
                        message.Attachments.Add(new Attachment(file.FilePath));
                    }
                    else
                    {
                        Stream fileStream = File.OpenRead(file.FilePath);
                        var attachment = new Attachment(fileStream, file.FileName);
                        message.Attachments.Add(attachment);
                    }
                }
            }

            string bodyContent = email.Body;

            message.Subject = email.Subject;

            message.IsBodyHtml = true;
            if (!ApplicationEnvironment.DisableEmbedHtmlImages)
            {
                List<LinkedResource> resources = new List<LinkedResource>();
                string contentBackup = bodyContent;//take a backup for log
                MailService.EmbedImages(bodyContent, resources);//embed all resources in aleternative view
                if (resources.Count > 0)
                {
                    var view = System.Net.Mail.AlternateView.CreateAlternateViewFromString(bodyContent, new System.Net.Mime.ContentType("text/html"));//create html view
                    foreach (var r in resources) view.LinkedResources.Add(r);//add all resources
                    message.AlternateViews.Add(view);//add alternative view
                    email.Body = contentBackup;//set body content from backup for log
                }
                else
                {
                    message.Body = bodyContent;
                }
            }
            else
            {
                message.Body = bodyContent;
            }

            return message;
        }

        internal static ItmSmtpClient GetTestMailServerSettings(EmailServerModel mailserver, string senderEmail, string senderName)
        {
            ItmSmtpClient mailClient = new ItmSmtpClient();

            mailClient.MailServerId = mailserver.Id;
            if (mailserver.SenderOptionId == EmailServerSenderOption.Sendemailusingserverdetail)
            {
                mailClient.From = senderEmail;
                mailClient.SenderName = senderName;
            }
            else
            {
                mailClient.From = mailserver.SenderId;
                mailClient.SenderName = mailserver.SenderName;
            }
            mailClient.UseDefaultCredentials = false;
            mailClient.Credentials = new System.Net.NetworkCredential(mailserver.UserName, Encryption.Decrypt(mailserver.Password));
            mailClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            mailClient.EnableSsl = mailserver.UseSslforOutgoing;
            mailClient.Host = mailserver.OutgoingServer;
            mailClient.Port = mailserver.OutgoingPort == 0 ? 25 : mailserver.OutgoingPort;
            mailClient.ReplyToEmailAddress = mailserver.ReplyToEmailAddress;
            mailClient.CopyToEmailAddress = mailserver.CopyToEmailAddress;
            return mailClient;
        }

        internal static MailMessage GetTestMailMessage(ItmSmtpClient smtp, EmailServerModel email)
        {
            MailMessage message = new MailMessage();

            message.From = new MailAddress(smtp.From, smtp.SenderName);

            message.To.Add(new MailAddress(smtp.From, smtp.SenderName));

            if (!String.IsNullOrEmpty(smtp.CopyToEmailAddress))
            {
                foreach (var address in MailService.GetEmailAddresses(smtp.CopyToEmailAddress))
                {
                    message.Bcc.Add(address);
                }
            }

            if (!String.IsNullOrEmpty(smtp.ReplyToEmailAddress))
            {
                foreach (var address in MailService.GetEmailAddresses(smtp.ReplyToEmailAddress))
                {
                    message.ReplyToList.Add(address);
                }
            }

            message.Body = "This is an email message sent automatically while testing the settings for your email server account. Please do not reply to this message.";
            message.Subject = "Test Message - Testing the settings for email server account";

            message.IsBodyHtml = false;
           
            return message;
        }

    }
}
