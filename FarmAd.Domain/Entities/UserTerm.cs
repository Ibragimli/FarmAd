using FarmAd.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Domain.Entities
{
    public class UserTerm : BaseEntity
    {
        public string Title { get; set; }
        public string Text { get; set; }
    }
}
