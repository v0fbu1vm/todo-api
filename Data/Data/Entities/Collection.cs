namespace Todo.Api.Data.Data.Entities
{
    /// <summary>
    /// Represents a collection. A placeholder for assignments/todos.
    /// </summary>
    public class Collection : BaseEntity
    {
        /// <summary>
        /// Represents the name of the collection.
        /// </summary>
        public string Name { get; set; } = default!;
        /// <summary>
        /// Represents the id of the user that owns this collection.
        /// </summary>
        public string UserId { get; set; } = default!;
        /// <summary>
        /// Represents the user that owns this collection.
        /// </summary>
        public virtual User User { get; set; } = default!;
        /// <summary>
        /// Represents the assignments belonging to this collection.
        /// </summary>
        public virtual ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();
    }
}
