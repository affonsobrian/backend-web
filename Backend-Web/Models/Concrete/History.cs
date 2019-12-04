using System.Collections.Generic;

namespace Backend_Web.Models
{
    public class History
    {
        #region .: Properties :.
        public int Id { get; set; }
        public int PersonId { get; set; }
        #endregion

        #region .: References :.
        public virtual List<Transaction> Transactions { get; set; }
        public virtual Person Person { get; set; } 
        #endregion


    }
}