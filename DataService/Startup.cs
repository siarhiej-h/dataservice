using Microsoft.AspNetCore.Builder;
using Common.Logging;
using DataService.Core;
using DataService.Core.Entities;
using DataService.HostConfiguration;
using DataService.Storage.Mongo;
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
                var mongoRepo = new MongoRepository<string, Item>(ConfigurationManager.GetValue<string>("mongo.host"));
                container.Register<IRepository<string, Item>, MongoRepository<string, Item>>(mongoRepo);

                //CORS Enable
                pipelines.AfterRequest.AddItemToEndOfPipeline((ctx) =>
                {
                    ctx.Response.WithHeader("Access-Control-Allow-Origin", "*")
                                    .WithHeader("Access-Control-Allow-Methods", "POST,GET,PUT,DELETE")
                                    .WithHeader("Access-Control-Allow-Headers", "Accept, Origin, Content-type")
                                    .WithHeader("Vary", "Origin");

                });

                pipelines.OnError.AddItemToEndOfPipeline(((context, exception) =>
                {
                    Log.Error(exception);
                    return HttpStatusCode.InternalServerError;
                }));
            }
        }
    }
}
