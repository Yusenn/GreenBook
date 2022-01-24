using System.Collections.Generic;

namespace GreenBook.Client.Shared.Domain
{
    public class Comment : BaseDomainModel
    {
        public string Text { get; set; }
        public virtual List<Post>Posts { get; set; }
    }
}