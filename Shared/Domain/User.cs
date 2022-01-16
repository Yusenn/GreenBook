using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreenBook.Client.Shared.Domain
{
    public class User : BaseDomainModel

    {
        public string Name { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Education { get; set; }
        public string Job { get; set; }
        public int PostId { get; set; }
        public virtual Post Post { get; set; }
        public int CommentId { get; set; }
        public virtual Comment Comment { get; set; }



    }
}
