using Microsoft.AspNetCore.Mvc.Filters;

namespace Softcode.GTex.Web.Api
{
    public interface IExceptionFilter : IFilterMetadata
    {
        void OnException(ExceptionContext context);
    }
}
