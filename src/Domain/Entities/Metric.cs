using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
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
            TimestampUtc = DateTime.UtcNow;
            MetricType = metricType;
            StatusCode = 200;
            PollId = pollId;
        }

        public long Id { get; set; }

        [Required]
 
        public DateTime TimestampUtc { get; set; }

        [Required]
        public MetricType MetricType { get; set; }

        [Required]
        
        public Guid PollId { get; set; }

        public int StatusCode { get; set; }

        public string Value { get; set; }
        public string Detail { get; set; }
    }
}
