using Microsoft.AspNetCore.Builder;
using Common.Logging;
using DataService.Core;
using DataService.Core.Entities;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Owin;
using Nancy.TinyIoc;

namespace DataService
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.UseOwin(x => x.UseNancy(opt => opt.Bootstrapper = new CustomBootstrapper()));
        }

        private class CustomBootstrapper : DefaultNancyBootstrapper
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
}
