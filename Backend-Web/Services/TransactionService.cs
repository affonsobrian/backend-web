using Backend_Web.DAL.DAO_s;
using Backend_Web.Models;
using Backend_Web.Utils;

namespace Backend_Web.Services
{
    public class TransactionService : CRUDService<Transaction, TransactionDAO>
    {
        public override BaseResponse<string> Insert(Transaction element)
        {
            return base.Insert(element);
        }
    }
}