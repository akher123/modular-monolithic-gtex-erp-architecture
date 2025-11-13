using Microsoft.Extensions.DependencyInjection;
using Softcode.GTex.ExceptionHelper;
using System;

namespace Softcode.GTex.ApploicationService
{
    public class ApplicationDependencyResolver
    {
        private readonly IServiceProvider serviceProvider;

        private static ApplicationDependencyResolver applicationDependencyResolver;

        public static ApplicationDependencyResolver Current
        {
            get
            {
                if (applicationDependencyResolver == null)
                {
                    throw new SoftcodeInvalidDataException("AppDependencyResolver not initialized. You should initialize it in Startup class");
                }
                return applicationDependencyResolver;
            }
        }

        public static void Init(IServiceProvider services)
        {
            applicationDependencyResolver = new ApplicationDependencyResolver(services);
        }

        private ApplicationDependencyResolver(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }
        

        public object GetService(Type serviceType)
        {
            return serviceProvider.GetService(serviceType);
        }

        public T GetService<T>()
        {
            return serviceProvider.GetService<T>();
        }
    }
}
