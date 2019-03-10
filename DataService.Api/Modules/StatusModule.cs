using DataService.Core;
using DataService.Core.Entities;
using Nancy;
using Nancy.ModelBinding;

namespace DataService.Api.Modules
{
    public sealed class StatusModule : NancyModule
    {
        public StatusModule()
            : base("api/status")
        {
            Post("/", o =>
            {
                var item = this.Bind<Item>();
                System.Console.WriteLine($"{item.Name} : {item.Price}");
                return HttpStatusCode.OK;
            });
        }
    }
}
