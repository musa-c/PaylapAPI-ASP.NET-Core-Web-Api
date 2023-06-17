using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paylap.Entities
{
    public class Comment
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }
        public User? User { get; set; } // bir yorumda bir kullanıcı vardır. 1-1
        public required string Text { get; set; }
        public Post? Post { get; set; } // bir yorum bir post ile ilişkilidir. 1-1
    }
}
