using System.Linq;
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
            var repo = new Repository<string, string>();
            repo.Create(key, value);
            Assert.Equal(value, repo.Read(key));
        }

        [Theory]
        [InlineData("key", "value")]
        public void RepositoryUpdateTest(string key, string value)
        {
            var repo = new Repository<string, string>();
            repo.Create(key, value);

            string newValue = "newValue";
            repo.Update(key, newValue);
            Assert.Equal(newValue, repo.Read(key));
        }

        [Theory]
        [InlineData("key", "value")]
        public void RepositoryDeleteTest(string key, string value)
        {
            var repo = new Repository<string, string>();
            repo.Create(key, value);

            repo.Delete(key);
            Assert.Null(repo.Read(key));
        }

        [Theory]
        [InlineData("key", "value")]
        public void RepositoryReadAllTest(string key, string value)
        {
            var repo = new Repository<string, string>();
            repo.Create(key, value);
            Assert.Equal(value, repo.ListAll().Single());
        }
    }
}
