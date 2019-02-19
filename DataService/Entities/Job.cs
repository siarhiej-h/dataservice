using System;
using System.Collections.Generic;
using System.Text;

namespace DataService.Entities
{
    public class Job
    {
        public Job(string jobName, JobClass @class)
        {
            JobName = jobName;
            Class = @class;
        }

        public string JobName { get; }

        public JobClass Class { get; }

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
