using DevExtreme.AspNet.Data.ResponseModel;
using Softcode.GTex.Api.Models;
using Softcode.GTex.Api.Providers;
using Softcode.GTex.ApploicationService;
using Softcode.GTex.ApploicationService.Models;
using Softcode.GTex.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Softcode.GTex.Api.Providers
{
    public abstract class BaseViewModel
    {
        public BaseViewModel()
        {

        }
        public int EntityType { get; set; }

        public bool IsDefaultBusinessProfile { get; set; } = false;
        

    }
}
