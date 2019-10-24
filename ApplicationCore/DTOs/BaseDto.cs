namespace ApplicationCore.DTOs
{
    /// <summary>
    /// Base class for DTO's
    /// </summary>
    public abstract class BaseDto
    {
        /// <summary>
        /// Primary Key
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// TotalRows
        /// </summary>
        public int TotalRows { get; set; }
    }
}
