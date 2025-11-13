using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Softcode.GTex.Web.Api.ApplicationService
{
    public static class ExecuteJobExtensions
    {
        public static void ExecuteJob(this IApplicationBuilder app,  IHangfireService hangfireService)
        {

            RecurringJob.AddOrUpdate("EmailJobQueueService",
               () => hangfireService.StartEmailJobQueueService(), Cron.Minutely);

            RecurringJob.AddOrUpdate("EmailQueueService",
              () => hangfireService.StartEmailQueueService(), Cron.Minutely);
        }
    }
}
