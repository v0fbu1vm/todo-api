namespace Todo.Api.Data.Data.Models.Assignment
{
    /// <summary>
    /// A model for creating a new <see cref="Todo.Api.Data.Data.Entities.Assignment"/>
    /// </summary>
    public class CreateAssignmentRequest
    {
        /// <summary>
        /// Represents the title of the assignment.
        /// </summary>
        public string Title { get; set; } = default!;

        /// <summary>
        /// Represents the description of the assignment.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Represents when the assignment is scheduled to occur.
        /// </summary>
        public DateTime DateScheduled { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Represents whether an assignment is important or not.
        /// </summary>
        public bool? IsImportant { get; set; }

        /// <summary>
        /// Represents the id of the collection that holds this assignment.
        /// </summary>
        public string CollectionId { get; set; } = default!;
    }
}