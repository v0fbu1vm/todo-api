using HotChocolate.AspNetCore.Authorization;
using Todo.Api.Data.Data.Entities;
using Todo.Api.Data.Services;

namespace Todo.Api.GraphQL.Queries
{
    [ExtendObjectType(OperationTypeNames.Query)]
    public class CollectionQueries
    {
        #region GetCollectionByIdAsync
        /// <summary>
        /// Gets a collection by id.
        /// </summary>
        /// <param name="id">Represents the id of the <see cref="Collection"/>.</param>
        /// <param name="service">A service for <see cref="Collection"/> related operations.</param>
        /// <returns>
        /// A <see cref="Collection"/> if found.
        /// </returns>
        [Authorize]
        public async Task<Collection?> GetCollectionByIdAsync(string id, [Service] CollectionService service)
        {
            return await service.GetCollectionByIdAsync(id);
        }
        #endregion

        #region GetCollectionByNameAsync
        /// <summary>
        /// Gets a collection by id.
        /// </summary>
        /// <param name="name">Represents the id of the <see cref="Collection"/>.</param>
        /// <param name="service">A service for <see cref="Collection"/> related operations.</param>
        /// <returns>
        /// A <see cref="Collection"/> if found.
        /// </returns>
        [Authorize]
        public async Task<Collection?> GetCollectionByNameAsync(string name, [Service] CollectionService service)
        {
            return await service.GetCollectionByNameAsync(name);
        }
        #endregion

        #region GetCollectionsAsync
        /// <summary>
        /// Gets a list of <see cref="Collection"/>.
        /// </summary>
        /// <param name="service">A service for <see cref="Collection"/> related operations.</param>
        /// <returns>
        /// A list of <see cref="Collection"/>'s.
        /// </returns>
        [Authorize]
        public async Task<ICollection<Collection>> GetCollectionsAsync([Service] CollectionService service)
        {
            return await service.GetCollectionsAsync();
        }
        #endregion
    }
}
