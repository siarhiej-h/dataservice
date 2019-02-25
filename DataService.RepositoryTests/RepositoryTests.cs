using System;
using System.Runtime.CompilerServices;
using DataService.Core.Entities;
using DataService.Storage;
using Xunit;

namespace DataService.RepositoryTests
{
    public class RepositoryTests
    {
        [Theory]
        [InlineData("key", "value")]
        public void RepositoryWriteTest(string key, string value)
        {
            var repo = new Repository<string, TestEntity>();
            var entity = new TestEntity(key, value);
            repo.Create(entity);
            Assert.Equal(entity, repo.Read(key));
        }

        [Theory]
        [InlineData("key", "value")]
        public void RepositoryUpdateTest(string key, string value)
        {
            var repo = new Repository<string, TestEntity>();
            repo.Create(new TestEntity(key, value));

            string newValue = "newValue";
            var newEntity = new TestEntity(key, newValue);
            repo.Update(newEntity);
            Assert.Equal(newEntity, repo.Read(key));
        }

        [Theory]
        [InlineData("key", "value")]
        public void RepositoryDeleteTest(string key, string value)
        {
            var repo = new Repository<string, TestEntity>();
            repo.Create(new TestEntity(key, value));

            repo.Delete(key);
            Assert.Null(repo.Read(key));
        }

        private class TestEntity : Tuple<string, string>, IEntity<string>
        {
            public string GetKey() => Item1;

            public TestEntity(string key, string value)
                : base(key, value)
            {
            }
        }
    }
}
