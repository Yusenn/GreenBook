using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GreenBook.Client.Shared.Domain
{
    public class Comment : BaseDomainModel
    {
        [Required]
        public string Text { get; set; }
        public int PostId { get; set; }
        public virtual Post Post { get; set; }
    }
}