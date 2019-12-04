using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend_Web.Models.ApiBody
{
    public class Return
    {
        public List<int> Properties { get; set; }
        public int Person { get; set; }
        public string Email { get; set; }
        public string Date { get; set; }
    }
}