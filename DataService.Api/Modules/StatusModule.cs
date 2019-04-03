using Nancy;

namespace DataService.Api.Modules
{
    public sealed class StatusModule : NancyModule
    {
        public StatusModule()
            : base("api/status")
        {
            Get("/", o =>
            {
                return Response.AsJson("I am alive");
            });
        }
    }
}
