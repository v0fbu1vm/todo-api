using HotChocolate.AspNetCore.Authorization;
using Todo.Api.Data.Data.Entities;
using Todo.Api.Data.Services;

namespace Todo.Api.GraphQL.Queries
{
    [ExtendObjectType(OperationTypeNames.Query)]
    public class AssignmentQueries
    {
        #region GetAssignmentByIdAsync

        /// <summary>
        /// Gets an <see cref="Assignment"/> by id.
        /// </summary>
        /// <param name="id">Represents the id of the <see cref="Assignment"/>.</param>.
        /// <param name="service">A service for <see cref="Assignment"/> related operations.</param>.
        /// <returns>
        /// An <see cref="Assignment"/> if found.
        /// </returns>
        [Authorize]
        public async Task<Assignment?> GetAssignmentByIdAsync(string id, [Service] AssignmentService service)
        {
            return await service.GetAssignmentByIdAsync(id);
        }

        #endregion GetAssignmentByIdAsync

        #region GetAssignmentsByCollectionIdAsync

        /// <summary>
        /// Gets a list of <see cref="Assignment"/>'s. Within a <see cref="Collection"/>.
        /// </summary>
        /// <param name="collectionId">Represents the id of the <see cref="Collection"/>.</param>
        /// <param name="service">A service for <see cref="Assignment"/> related operations.</param>.
        /// <returns>
        /// A list of <see cref="Assignment"/>'s
        /// </returns>
        [Authorize]
        public async Task<ICollection<Assignment>> GetAssignmentsByCollectionIdAsync(string collectionId, [Service] AssignmentService service)
        {
            return await service.GetAssignmentsAsync(collectionId);
        }

        #endregion GetAssignmentsByCollectionIdAsync

        #region GetAssignmentsAsync

        /// <summary>
        /// Gets a list of <see cref="Assignment"/>'s.
        /// </summary>
        /// <param name="service">A service for <see cref="Assignment"/> related operations.</param>.
        /// <returns>
        /// A list of <see cref="Assignment"/>'s
        /// </returns>
        [Authorize]
        public async Task<ICollection<Assignment>> GetAssignmentsAsync([Service] AssignmentService service)
        {
            return await service.GetAssignmentsAsync();
        }

        #endregion GetAssignmentsAsync
    }
}