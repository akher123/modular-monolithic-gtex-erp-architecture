using Softcode.GTex.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Softcode.GTex.ApplicantionService.Messaging
{
    public interface IEmailProcessor
    {
        void PrepareEmailContentAsync(EmailJobQueue emailJobQueue, EmailQueue emailQueue);
    }
}
