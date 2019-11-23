using System.ComponentModel.DataAnnotations;

namespace Backend_Web.Models
{
    /// <summary>
    /// The Properties that can be borrowed
    /// </summary>
    public class Property
    {
        #region .: Properties :.
        /// <summary>
        /// The identification of the property
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The name of the property
        /// </summary>
        public string ServiceTag { get; set; }
        /// <summary>
        /// The description of the property
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// The status of the propety
        /// </summary>
        public string Status { get; set; }
        #endregion

        /// <summary>
        /// Type of property
        /// </summary>
        public string Type { get; set; }


        #region .: References :.

        /// <summary>
        /// The id of the person with this item
        /// </summary>
        public int? PersonId { get; set; }

        #endregion
    }
}