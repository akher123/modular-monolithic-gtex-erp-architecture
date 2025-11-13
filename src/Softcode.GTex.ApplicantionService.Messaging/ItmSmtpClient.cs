using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Softcode.GTex.ApplicantionService.Messaging
{
    public class ItmSmtpClient : SmtpClient
    {
        /// <summary>
        /// Get or set mail server id
        /// </summary>
        public int MailServerId { get; set; }
        /// <summary>
        /// get or set from
        /// </summary>
        public string From
        {
            get;
            set;
        }
        /// <summary>
        /// get or set sender name
        /// </summary>
        public string SenderName
        {
            get;
            set;
        }
        /// <summary>
        /// get or set reply to email address
        /// </summary>
        public string ReplyToEmailAddress { get; set; }
        /// <summary>
        /// get or set copy to email address
        /// </summary>
        public string CopyToEmailAddress { get; set; }
    }
}
