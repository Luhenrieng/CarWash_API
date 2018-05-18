
namespace BasicDDD.BasicApplication.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Domain.Interfaces.Service;
    using Domain.Services;
    using Application;
    using Application.Interface;
    using Domain.Interfaces;
    using Infra.Data.Repositories;
    using System.Web.Http;
    using System.Collections.Generic;
    using System.Web.Http.Dependencies;
    using Ninject.Syntax;
    using System.Diagnostics.Contracts;
    using Ninject.Modules;
    using Domain.Interfaces.Repositories;

    public class NinjectRegistrations : NinjectModule
    {
        public override void Load()
        {
            Bind<IClientService>().To<ClientService>();
            Bind<IClientAppService>().To<ClientAppService>();
            Bind<IClientRepository>().To<ClientRepository>();

            Bind<IServiceService>().To<ServiceService>();
            Bind<IServiceAppService>().To<ServiceAppService>();
            Bind<IServiceRepository>().To<ServiceRepository>();

            Bind<IUserService>().To<UserService>();
            Bind<IUserAppService>().To<UserAppService>();
            Bind<IUserRepository>().To<UserRepository>();

            Bind<ILogService>().To<LogService>();
            Bind<ILogAppService>().To<LogAppService>();
            Bind<ILogRepository>().To<LogRepository>();

            Bind<IUserTokenService>().To<UserTokenService>();
            Bind<IUserTokenAppService>().To<UserTokenAppService>();
            Bind<IUserTokenRepository>().To<UserTokenRepository>();
        }
    }


    public class NinjectDependencyScope : IDependencyScope
    {
        private IResolutionRoot resolver;

        internal NinjectDependencyScope(IResolutionRoot resolver)
        {
            Contract.Assert(resolver != null);

            this.resolver = resolver;
        }

        public void Dispose()
        {
            IDisposable disposable = resolver as IDisposable;
            if (disposable != null)
                disposable.Dispose();

            resolver = null;
        }

        public object GetService(Type serviceType)
        {
            if (resolver == null)
                throw new ObjectDisposedException("this", "This scope has already been disposed");

            return resolver.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            if (resolver == null)
                throw new ObjectDisposedException("this", "This scope has already been disposed");

            return resolver.GetAll(serviceType);
        }
    }


    public class NinjectDependencyResolver : NinjectDependencyScope, IDependencyResolver, System.Web.Mvc.IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernel)
            : base(kernel)
        {
            this.kernel = kernel;
        }

        public IDependencyScope BeginScope()
        {
            return new NinjectDependencyScope(kernel.BeginBlock());
        }
    }
}
