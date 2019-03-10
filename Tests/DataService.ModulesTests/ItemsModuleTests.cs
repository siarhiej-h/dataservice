using System.Collections.Generic;
using System.Linq;
using DataService.Api.Modules;
using DataService.Core;
using DataService.Core.Entities;
using Nancy;
using Nancy.Testing;
using NSubstitute;
using Xunit;

namespace DataService.ModulesTests
{
    public class ItemsModuleTests
    {
        private const decimal TheUltimateAnswer = 42m;

        [Fact]
        public void GetAllShouldReturnStatusOk()
        {
            // Given
            var repo = Substitute.For<IRepository<string, Item>>();
            repo.ListAll().Returns(ci => new [] { new Item("id", TheUltimateAnswer) });

            var browser = new Browser(configurator =>
            {
                configurator.Module<ItemsModule>();
                configurator.Dependency(repo);
            });

            // When
            var result = browser.Get("/api/items/", with => {
                with.HttpRequest();
            });

            // Then
            Assert.Equal(HttpStatusCode.OK, result.Result.StatusCode);

            var items = result.Result.Body.DeserializeJson<IList<Item>>();
            var item = items.Single();
            Assert.Equal("id", item.Name);
            Assert.Equal(TheUltimateAnswer, item.Price);
        }

        [Fact]
        public void GetShouldReturnStatusOk()
        {
            // Given
            var repo = Substitute.For<IRepository<string, Item>>();
            repo.Read(Arg.Any<string>()).Returns(ci => new Item(ci.ArgAt<string>(0), TheUltimateAnswer));

            var browser = new Browser(configurator =>
            {
                configurator.Module<ItemsModule>();
                configurator.Dependency(repo);
            });

            // When
            var result = browser.Get("/api/items/id", with => {
                with.HttpRequest();
            });

            // Then
            Assert.Equal(HttpStatusCode.OK, result.Result.StatusCode);

            var item = result.Result.Body.DeserializeJson<Item>();
            Assert.Equal("id", item.Name);
            Assert.Equal(TheUltimateAnswer, item.Price);
        }

        [Fact]
        public void PostShouldReturnStatusOk()
        {
            // Given
            var repo = Substitute.For<IRepository<string, Item>>();
            var browser = new Browser(configurator =>
            {
                configurator.Module<ItemsModule>();
                configurator.Dependency(repo);
            });

            var item = new Item("id", TheUltimateAnswer);

            // When
            var result = browser.Post("/api/items", with => {
                with.HttpRequest();
                with.JsonBody(item);
            });

            // Then
            Assert.Equal(HttpStatusCode.OK, result.Result.StatusCode);
            repo.Received()
                .Create(Arg.Is<Item>(j => j.Name == item.Name && j.Price == item.Price));
        }

        [Fact]
        public void PutShouldReturnStatusOk()
        {
            // Given
            var repo = Substitute.For<IRepository<string, Item>>();
            var browser = new Browser(configurator =>
            {
                configurator.Module<ItemsModule>();
                configurator.Dependency(repo);
            });

            var item = new Item("id", TheUltimateAnswer);

            // When
            var result = browser.Put("/api/items", with => {
                with.HttpRequest();
                with.JsonBody(item);
            });

            // Then
            Assert.Equal(HttpStatusCode.OK, result.Result.StatusCode);

            repo.Received()
                .Update(Arg.Is<Item>(j => j.Name == item.Name && j.Price == item.Price));
        }

        [Fact]
        public void DeleteShouldReturnStatusOk()
        {
            // Given
            var repo = Substitute.For<IRepository<string, Item>>();
            repo.Read(Arg.Any<string>()).Returns(ci => new Item(ci.ArgAt<string>(0), TheUltimateAnswer));

            var browser = new Browser(configurator =>
            {
                configurator.Module<ItemsModule>();
                configurator.Dependency(repo);
            });

            // When
            var result = browser.Delete("/api/items/id", with => {
                with.HttpRequest();
            });

            // Then
            Assert.Equal(HttpStatusCode.OK, result.Result.StatusCode);

            repo.Received().Delete("id");
        }
    }
}
