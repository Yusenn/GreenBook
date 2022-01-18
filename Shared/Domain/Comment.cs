using GreenBook.Client.Shared.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenBook.Shared.Domain
{
    public class Comment : BaseDomainModel
    {
        public string Text { get; set; }

    }

}
