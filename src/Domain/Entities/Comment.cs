using System;
using System.ComponentModel.DataAnnotations;

using System.Linq;
namespace ComeTogether.Domain.Entities
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }
        public string UserId { get; set; }
        public int ArticleId { get; set; }
        [Required]
        public string Content { get; set; }
        [DataType(DataType.Date)]
        public DateTime? PostDate { get; set; }
        public virtual Article Articles { get; set; }
    }
}