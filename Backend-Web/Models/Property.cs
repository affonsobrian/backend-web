namespace Backend_Web.Models
{
    /// <summary>
    /// The Properties that can be borrowed
    /// </summary>
    public class Property
    {
        /// <summary>
        /// The identification of the property
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The name of the property
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The description of the property
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// The id of the person with this item
        /// </summary>
        public int PersonId { get; set; }
    }
}