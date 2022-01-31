using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GreenBook.Client.Shared.Domain
{
    public class Post : BaseDomainModel
    {   
        [Required]
        public string Title { get; set; }
        [Required]
        public string Text { get; set; }
        public string Location { get; set; }
        public string PicUrl { get; set; }
        public string UserId { get; set; }
        public virtual List<Comment> Comments { get; set; }
    }
}
    