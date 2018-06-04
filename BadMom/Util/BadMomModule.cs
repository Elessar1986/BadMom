using BadMom.BLL.Interfaces;
using BadMom.BLL.Services;
using Ninject;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BadMom.Util
{
    //public class BadMomModule : NinjectModule
    //{
    //    public override void Load()
    //    {
    //        Bind<IBadMomDataService>().To<BadMomDataService>();
    //    }
    //}

    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;
        public NinjectDependencyResolver()
        {
            kernel = new StandardKernel();
            AddBindings();
        }
        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
        private void AddBindings()
        {
            kernel.Bind<IBadMomDataService>().To<BadMomDataService>().WithConstructorArgument("BadMomResource");
        }
    }
}