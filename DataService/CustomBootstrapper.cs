using System;
using System.Collections.Generic;
using System.Text;
using Common.Logging;
using DataService.Core;
using DataService.Entities;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;

namespace DataService
{
    internal class CustomBootstrapper : DefaultNancyBootstrapper
    {
        private static readonly ILog Log = LogManager.GetLogger<CustomBootstrapper>();

        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            container.Register<IRepository<string, Job>, Repository<string, Job>>().AsSingleton();

            pipelines.OnError.AddItemToEndOfPipeline(((context, exception) =>
            {
                Log.Error(exception);
                return HttpStatusCode.InternalServerError;
            }));
        }
    }
}
