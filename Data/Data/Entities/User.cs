using Microsoft.AspNetCore.Identity;

namespace Todo.Api.Data.Data.Entities
{
    /// <summary>
    /// Represents a user.
    /// </summary>
    public class User : IdentityUser
    {
        /// <summary>
        /// Represents the name of the user.
        /// </summary>
        public string Name { get; set; } = default!;
        /// <summary>
        /// Represents when a record was added to the system.
        /// </summary>
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        /// <summary>
        /// Represents when a record was modified.
        /// </summary>
        public DateTime DateModified { get; set; } = DateTime.UtcNow;
        /// <summary>
        /// Represents a list of collections belonging to this user.
        /// </summary>
        public virtual ICollection<Collection> Collections { get; set; } = new List<Collection>();
        /// <summary>
        /// Represents a list of assignments, assigned to this user.
        /// </summary>
        public virtual ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();
    }
}
