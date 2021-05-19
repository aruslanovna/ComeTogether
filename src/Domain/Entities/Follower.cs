using System.ComponentModel.DataAnnotations;

namespace ComeTogether.Domain.Entities
{
    public class Follower
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public string FollowerId { get; set; }
    }
}
