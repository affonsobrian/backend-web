using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Backend_Web.Models
{
    [Table("Person")]
    public class Person
    {
        /// <summary>
        /// Id for selecting the person
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// First name of the person
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Last name of the person
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// Email for contacting the person
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// RG (Document) for controlling the registration in the system
        /// </summary>
        public string RG { get; set; }
        /// <summary>
        /// Telephone for contacting the person
        /// </summary>
        public string Telephone { get; set; }
        /// <summary>
        /// Wheter the person is still active
        /// </summary>
        public bool Active { get; set; } = true;
    }
}