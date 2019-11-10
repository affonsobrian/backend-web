using System.Collections.Generic;

namespace Backend_Web.Models.ApiBody
{
    public class Borrow
    {
        public List<int> Properties { get; set; }
        public int PersonId { get; set; }
        public string Email { get; set; }
        public string Date { get; set; }
        public string Image { get; set; }
    }
}