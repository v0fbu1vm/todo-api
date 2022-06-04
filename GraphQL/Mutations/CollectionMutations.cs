using HotChocolate.AspNetCore.Authorization;
using Todo.Api.Data.Data.Entities;
using Todo.Api.Data.Data.Models.Collection;
using Todo.Api.Data.Data.Models.Response;
using Todo.Api.Data.Data.Objects;
using Todo.Api.Data.Services;

namespace Todo.Api.GraphQL.Mutations
{
    [ExtendObjectType(OperationTypeNames.Mutation)]
    public class CollectionMutations
    {
        #region CreateCollectionAsync
        /// <summary>
        /// Adds a new collection.
        /// </summary>
        /// <param name="request">Represents the required data for creating a new <see cref="Collection"/>.</param>
        /// <param name="service">A service for <see cref="Collection"/> related operations.</param>
        /// <returns>
        /// A<see cref="Response{Collection}"/>, containing the details of operation.
        /// </returns>
        [Authorize]
        public async Task<Response<Collection>> CreateCollectionAsync(CreateCollectionRequest request, [Service] CollectionService service)
        {
            var result = await service.CreateCollectionAsync(request);

            if (result.Succeeded)
            {
                return Response<Collection>.Created(result.Result);
            }

            return result.Fault.ErrorCode switch
            {
                ExceptionCodes.Code401Unauthorized => Response<Collection>.Unauthorized(result.Fault.ErrorMessage),
                _ => Response<Collection>.BadRequest(result.Fault.ErrorMessage),
            };
        }
        #endregion
    }
}
