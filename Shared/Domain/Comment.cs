using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreenBook.Client.Shared.Domain
{
    public class Comment:BaseDomainModel
    {
        public string Text { get; set; }
        public virtual List<Post> Post { get; set; }
        public virtual List<User> User { get; set; }
    }
}
