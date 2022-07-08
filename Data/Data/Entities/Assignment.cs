namespace Todo.Api.Data.Data.Entities
{
    /// <summary>
    /// Represents an assignment.
    /// </summary>
    public class Assignment : BaseEntity
    {
        /// <summary>
        /// Represents the title of the assignment.
        /// </summary>
        public string Title { get; set; } = default!;

        /// <summary>
        /// Represents the description of the assignment.
        /// </summary>
        public string Description { get; set; } = default!;

        /// <summary>
        /// Represents when the assignment is scheduled to occur.
        /// </summary>
        public DateTime DateScheduled { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Represents whether an assignment is complete or not.
        /// </summary>
        public bool IsCompleted { get; set; } = false;

        /// <summary>
        /// Represents whether an assignment is important or not.
        /// </summary>
        public bool IsImportant { get; set; } = false;

        /// <summary>
        /// Represents the id of the collection that holds this assignment.
        /// </summary>
        public string CollectionId { get; set; } = default!;

        /// <summary>
        /// Represents the collection that holds this assignment.
        /// </summary>
        public virtual Collection Collection { get; set; } = default!;

        /// <summary>
        /// Represents the id of the user that is assigned this assignment.
        /// </summary>
        public string UserId { get; set; } = default!;

        /// <summary>
        /// Represents the user that is assigned this assignment.
        /// </summary>
        public virtual User User { get; set; } = default!;
    }
}