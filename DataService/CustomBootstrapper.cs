using System;
using System.Collections.Generic;
using System.Text;
using DataService.Core;
using DataService.Entities;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;

namespace DataService
{
    internal class CustomBootstrapper : DefaultNancyBootstrapper
    {
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            container.Register<IRepository<string, Job>, Repository<string, Job>>().AsSingleton();
        }
    }
}
