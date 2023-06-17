using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paylap.Entities
{
    public class User
    {
        public User()
        {
            // depedet entitiy nesne referansı üzerinden erişebilmek için null olmaması gerekiyor
            Posts = new HashSet<Post>();
        }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(20, ErrorMessage = "Adınız 20 karakteri aşamaz.")]
        public string? FirstName { get; set; }
        [StringLength(20, ErrorMessage = "Soyadınız 20 karakteri aşamaz.")]
        public string? LastName { get; set; }
        [StringLength(10, ErrorMessage = "Kullanıcı adınız 10 karakteri aşamaz.")]
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [StringLength(15, MinimumLength = 6, ErrorMessage = "Şifreniz en az 6 karakterden en çok 15 karakterden oluşmalıdır.")]
        public string Password { get; set; }
        public byte[]? Avatar { get; set; }
        // user'in birden fazla post'u olabilir. Post yalnızce bir kullanıca ait 1-n
        public virtual ICollection<Post> Posts { get; set; }
        public ICollection<BookMark>? BookMarks { get; set; }
        public ICollection<Comment>? Comments { get; set; }
        public ICollection<Dislike>? Dislikes { get; set; }
        public ICollection<Like>? Likes { get; set; }

    }
}
