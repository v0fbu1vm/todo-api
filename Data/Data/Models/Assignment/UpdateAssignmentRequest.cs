namespace Todo.Api.Data.Data.Models.Assignment
{
    /// <summary>
    /// A model for updating a <see cref="Todo.Api.Data.Data.Entities.Assignment"/>
    /// </summary>
    public class UpdateAssignmentRequest
    {
        public string Id { get; set; } = default!;

        /// <summary>
        /// Represents the title of the assignment.
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// Represents the description of the assignment.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Represents when the assignment is scheduled to occur.
        /// </summary>
        public DateTime? DateScheduled { get; set; }

        /// <summary>
        /// Represents whether an assignment is complete or not.
        /// </summary>
        public bool? IsCompleted { get; set; }

        /// <summary>
        /// Represents whether an assignment is important or not.
        /// </summary>
        public bool? IsImportant { get; set; }
    }
}