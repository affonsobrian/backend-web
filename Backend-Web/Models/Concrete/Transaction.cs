using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend_Web.Models
{
    public class Transaction
    {
        #region .: Properties :.

        public int Id { get; set; }
        public int PropertyId { get; set; }
        public string Document { get; set; }
        public DateTime Date { get; set; }
        public TransactionType Type { get; set; }

        #endregion

        #region .: References :.

        public virtual Property Property { get; set; } 

        #endregion
    }
    public enum TransactionType
    {
        Loan,
        Return
    }
}