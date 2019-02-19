using DataService.Core;
using DataService.Entities;
using Nancy;

namespace DataService.Modules
{
    public sealed class JobsModule : NancyModule
    {
        public JobsModule(IRepository<string, Job> repository)
            : base("api/jobs")
        {
            Get("/{id}", o =>
            {
                var job = repository.Read(o.id);
                return Response.AsJson((object) job);
            });
        }
    }
}
