using Todo.Api.Data.Data;
using Todo.Api.Data.Data.Entities;

namespace Todo.Api.GraphQL.Resolvers
{
    /// <summary>
    /// A resolver for <see cref="User"/> navigation properties.
    /// </summary>
    public class UserResolvers
    {
        #region GetAssignments

        /// <summary>
        /// Gets a list of <see cref="Assignment"/>'s assigned to this user.
        /// </summary>
        /// <param name="user">Represents a <see cref="User"/>.</param>
        /// <param name="context">DbContext for querying and mutating data.</param>
        /// <returns>
        /// A list of <see cref="Assignment"/>'s.
        /// </returns>
        public IQueryable<Assignment> GetAssignments([Parent] User user, [ScopedService] DatabaseContext context)
        {
            return context.Assignments.Where(options => options.UserId == user.Id).AsQueryable();
        }

        #endregion GetAssignments

        #region GetCollections

        /// <summary>
        /// Gets a list of <see cref="Collection"/>'s belonging to this user.
        /// </summary>
        /// <param name="user">Represents a <see cref="User"/>.</param>
        /// <param name="context">DbContext for querying and mutating data.</param>
        /// <returns>
        /// A list of <see cref="Collection"/>'s.
        /// </returns>
        public IQueryable<Collection> GetCollections([Parent] User user, [ScopedService] DatabaseContext context)
        {
            return context.Collections.Where(options => options.UserId == user.Id).AsQueryable();
        }

        #endregion GetCollections
    }
}