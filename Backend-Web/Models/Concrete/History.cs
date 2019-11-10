using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend_Web.Models
{
    public class History
    {   
        public int Id { get; set; }
        public List<Transaction> Transactions { get; set; }

    }
}