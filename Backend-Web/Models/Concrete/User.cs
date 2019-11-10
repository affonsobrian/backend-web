using System.ComponentModel.DataAnnotations.Schema;

namespace Backend_Web.Models
{
    /// <summary>
    /// The model of the user
    /// </summary>
    [Table("User")]
    public class User
    {
        /// <summary>
        /// The identification of the user
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The Name of the user
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The password of  the user
        /// </summary>
        public string Password { get; set; }
    }
}