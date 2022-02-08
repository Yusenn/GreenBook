using GreenBook.Client.Shared.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenBook.Shared.Domain
{
    public class Collection :BaseDomainModel
    {
        [Required]
        public string CollectionName { get; set; }
        public int PostId { get; set; }
        public virtual Post Post { get; set; }
    }
}
