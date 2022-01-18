using GreenBook.Shared.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreenBook.Client.Shared.Domain
{
    public class Post : BaseDomainModel
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public string Location { get; set; }
        public string PicUrl { get; set; }
        public int CommentId { get; set; }
        public virtual Comment Comment { get; set; }
    }
}
