using DataService.Api.Modules;
using DataService.Core;
using DataService.Core.Entities;
using Nancy;
using Nancy.Testing;
using NSubstitute;
using Xunit;

namespace DataService.ModulesTests
{
    public class JobModuleTests
    {
        [Fact]
        public void GetShouldReturnStatusOk()
        {
            // Given
            var repo = Substitute.For<IRepository<string, Job>>();
            repo.Read(Arg.Any<string>()).Returns(ci => new Job(ci.ArgAt<string>(0), Job.JobClass.Awesome));

            var browser = new Browser(configurator =>
            {
                configurator.Module<JobsModule>();
                configurator.Dependency(repo);
            });

            // When
            var result = browser.Get("/api/jobs/id", with => {
                with.HttpRequest();
            });

            // Then
            Assert.Equal(HttpStatusCode.OK, result.Result.StatusCode);

            var job = result.Result.Body.DeserializeJson<Job>();
            Assert.Equal("id", job.JobName);
            Assert.Equal(Job.JobClass.Awesome, job.Class);
        }

        [Fact]
        public void PostShouldReturnStatusOk()
        {
            // Given
            var repo = Substitute.For<IRepository<string, Job>>();
            var browser = new Browser(configurator =>
            {
                configurator.Module<JobsModule>();
                configurator.Dependency(repo);
            });

            var job = new Job("id", Job.JobClass.Awesome);

            // When
            var result = browser.Post("/api/jobs/id", with => {
                with.HttpRequest();
                with.JsonBody(job);
            });

            // Then
            Assert.Equal(HttpStatusCode.OK, result.Result.StatusCode);
            repo.Received()
                .Create(job.JobName, Arg.Is<Job>(j => j.JobName == job.JobName && j.Class == job.Class));
        }

        [Fact]
        public void PutShouldReturnStatusOk()
        {
            // Given
            var repo = Substitute.For<IRepository<string, Job>>();
            var browser = new Browser(configurator =>
            {
                configurator.Module<JobsModule>();
                configurator.Dependency(repo);
            });

            var job = new Job("id", Job.JobClass.Awesome);

            // When
            var result = browser.Put("/api/jobs/id", with => {
                with.HttpRequest();
                with.JsonBody(job);
            });

            // Then
            Assert.Equal(HttpStatusCode.OK, result.Result.StatusCode);

            repo.Received()
                .Update(job.JobName, Arg.Is<Job>(j => j.JobName == job.JobName && j.Class == job.Class));
        }

        [Fact]
        public void DeleteShouldReturnStatusOk()
        {
            // Given
            var repo = Substitute.For<IRepository<string, Job>>();
            repo.Read(Arg.Any<string>()).Returns(ci => new Job(ci.ArgAt<string>(0), Job.JobClass.Awesome));

            var browser = new Browser(configurator =>
            {
                configurator.Module<JobsModule>();
                configurator.Dependency(repo);
            });

            // When
            var result = browser.Delete("/api/jobs/id", with => {
                with.HttpRequest();
            });

            // Then
            Assert.Equal(HttpStatusCode.OK, result.Result.StatusCode);

            repo.Received().Delete("id");
        }
    }
}
