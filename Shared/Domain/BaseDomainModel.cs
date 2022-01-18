using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreenBook.Client.Shared.Domain
{
    public abstract class BaseDomainModel
    { 
        public int Id { get; set; }
        public DateTime DateCreate { get; set;}
        public DateTime DateUpdate { get; set;}
        
        public string CreateBy { get; set; }

        public string UpdateBy { get; set; }

    }
}
