using DataService.Core;
using DataService.Entities;
using Nancy;
using Nancy.ModelBinding;

namespace DataService.Modules
{
    public sealed class JobsModule : NancyModule
    {
        public JobsModule(IRepository<string, Job> repository)
            : base("api/jobs")
        {
            Post("/{id}", o =>
            {
                var job = this.Bind<Job>();
                repository.Create(o.id, job);
                return HttpStatusCode.OK;
            });

            Get("/{id}", o =>
            {
                var job = repository.Read(o.id);
                return Response.AsJson((object)job);
            });

            Put("/{id}", o =>
            {
                var job = this.Bind<Job>();
                repository.Update(o.id, job);
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
