    using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend_Web.Models
{
    public class Token
    {
        public int Id { get; set; }
        public Guid Value { get; set; }
        public DateTime LastRequest { get; set; }
        public virtual User User { get; set; }
    }
}