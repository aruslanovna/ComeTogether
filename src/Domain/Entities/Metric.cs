using ComeTogether.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Index = Microsoft.EntityFrameworkCore.Metadata.Internal.Index;

namespace VotingApplication.Data.Model
{
    public class Metric
    {
        internal Metric() { }

        public Metric(MetricType metricType, Guid pollId)
        {
            //TimestampUtc = DateTime.UtcNow;
            //MetricType = metricType;
            //StatusCode = 200;
            //PollId = pollId;
            
            Projects = new HashSet<Project>();
            
        }
        public ICollection<Project> Projects { get; private set; }
        [Key]
   
        public long Id { get; set; }

        //[Required]
 
        //public DateTime TimestampUtc { get; set; }

        //[Required]
        //public MetricType MetricType { get; set; }

        //[Required]
        
        //public Guid PollId { get; set; }

        //public int StatusCode { get; set; }

        //public string Value { get; set; }
        //public string Detail { get; set; }




        public DateTime Date { get; set; }

        public int CPO { get; set; }
        public int CAC { get; set; }
        public int ROMI { get; set; }
        public int ROI { get; set; }
        public int ROAS { get; set; }
        public int ARPU { get; set; }
        public int AOV { get; set; }
        public int LTV { get; set; }
    }
}
