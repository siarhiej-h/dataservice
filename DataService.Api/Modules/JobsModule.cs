using System;
using DataService.Core;
using DataService.Core.Entities;
using Nancy;
using Nancy.ModelBinding;

namespace DataService.Api.Modules
{
    public sealed class JobsModule : NancyModule
    {
        public JobsModule(IRepository<string, Job> repository)
            : base("api/jobs")
        {
            Get("/", o =>
            {
                var jobs = repository.ListAll();
                return Response.AsJson((object)jobs);
            });

            Post("/{id}", o =>
            {
                var job = this.Bind<Job>();
                repository.Create(job);
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
                repository.Update(job);
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
