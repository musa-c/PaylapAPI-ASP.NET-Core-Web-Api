using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paylap.Entities
{
    public class Post
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; } // her post sadece bir kullanıcıya ait.
        public required string Text { get; set; }
        public int? LikeCount { get; set; }
        public int? CommentCount { get; set; }
        public int? BookMarkCount { get; set; }
        public int? DislikeCount { get; set; }
        public DateTime TimeStamp { get; set; }
        public ICollection<Comment>? Comments { get; set; } // bir postta birden fazla yorum olabilir 1-n ilişki var.
        public ICollection<Like>? Likes { get; set; }
        public ICollection<Dislike>? Dislikes { get; set; }
        public ICollection<BookMark>? BookMarks { get; set; }
    }
}
