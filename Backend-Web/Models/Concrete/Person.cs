using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend_Web.Models
{
    [Table("Person")]
    public class Person
    {
        #region .: Properties :.

        /// <summary>
        /// Id for selecting the person
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Full name of the person
        /// </summary>
        public string Name { get; set; }
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
        [JsonIgnore]
        public bool Active { get; set; } = true; 

        #endregion

        #region .: References :.

        /// <summary>
        /// Properties of this user
        /// </summary>
        [JsonIgnore]
        public virtual List<Property> Properties { get; set; } 
        
        #endregion
    }
}