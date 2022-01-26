using System.Collections.Generic;

namespace GreenBook.Client.Shared.Domain
{
    public class Comment : BaseDomainModel
    {
        public string Text { get; set; }
        public int PostId { get; set; }
        public virtual Post Post { get; set; }
    }
}