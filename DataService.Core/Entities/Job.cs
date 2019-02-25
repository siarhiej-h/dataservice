namespace DataService.Core.Entities
{
    public class Job : IEntity<string>
    {
        public Job()
        {
        }

        public Job(string jobName, JobClass @class)
        {
            JobName = jobName;
            Class = @class;
        }

        public string JobName { get; set; }

        public JobClass Class { get; set; }

        public string GetKey() => JobName;

        public enum JobClass
        {
            Awful,
            Shitty,
            Neutral,
            Cool,
            Awesome
        }
    }
}
