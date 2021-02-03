using System.ComponentModel.DataAnnotations;

namespace ComeTogether.Domain.Entities
{
    public class Following
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public string FollowingId { get; set; }
    }
}
