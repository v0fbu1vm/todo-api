using Todo.Api.Data.Data;
using Todo.Api.Data.Data.Entities;

namespace Todo.Api.GraphQL.Resolvers
{
    /// <summary>
    /// A resolver for <see cref="Assignment"/> navigation properties.
    /// </summary>
    public class AssignmentResolvers
    {
        #region GetUser
        /// <summary>
        /// Gets the <see cref="User"/> that owns this <see cref="Assignment"/>.
        /// </summary>
        /// <param name="assignment">Represents an <see cref="Assignment"/>.</param>
        /// <param name="context">DbContext for querying and mutating data.</param>
        /// <returns>
        /// A <see cref="User"/>.
        /// </returns>
        public User? GetUser([Parent] Assignment assignment, [ScopedService] DatabaseContext context)
        {
            return context.Users.FirstOrDefault(options => options.Id == assignment.UserId);
        }
        #endregion

        #region GetCollection
        /// <summary>
        /// Gets the <see cref="Collection"/> that holds this <see cref="Assignment"/>.
        /// </summary>
        /// <param name="assignment">Represents an <see cref="Assignment"/>.</param>
        /// <param name="context">DbContext for querying and mutating data.</param>
        /// <returns>
        /// A <see cref="Collection"/>.
        /// </returns>
        public Collection? GetCollection([Parent] Assignment assignment, [ScopedService] DatabaseContext context)
        {
            return context.Collections.FirstOrDefault(options => options.Id == assignment.CollectionId);
        }
        #endregion
    }
}
