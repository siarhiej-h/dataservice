using DataService.Core;
using DataService.Core.Entities;
using Nancy;
using Nancy.ModelBinding;

namespace DataService.Api.Modules
{
    public sealed class ItemsModule : NancyModule
    {
        public ItemsModule(IRepository<string, Item> repository)
            : base("api/items")
        {
            Get("/", o =>
            {
                var items = repository.ListAll();
                return Response.AsJson((object)items);
            });

            Post("/", o =>
            {
                var item = this.Bind<Item>();
                repository.Create(item);
                return HttpStatusCode.OK;
            });

            Get("/{id}", o =>
            {
                var item = repository.Read(o.id);
                return Response.AsJson((object)item);
            });

            Put("/", o =>
            {
                var item = this.Bind<Item>();
                repository.Update(item);
                return HttpStatusCode.OK;
            });

            Delete("/{id}", o =>
            {
                repository.Delete(o.id);
                return HttpStatusCode.OK;
            });
        }
    }
}
