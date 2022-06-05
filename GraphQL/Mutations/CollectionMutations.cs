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
        /// A <see cref="Response{Collection}"/>, containing the details of operation.
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

        #region UpdateCollectionAsync
        /// <summary>
        /// Updates a collection.
        /// </summary>
        /// <param name="id">Represents the id of the <see cref="Collection"/>.</param>
        /// <param name="request">Represents the required data for updating a <see cref="Collection"/>.</param>
        /// <param name="service">A service for <see cref="Collection"/> related operations.</param>
        /// <returns>
        /// A <see cref="Response{Collection}"/>, containing the details of operation.
        /// </returns>
        [Authorize]
        public async Task<Response<Collection>> UpdateCollectionAsync(string id, UpdateCollectionRequest request, [Service] CollectionService service)
        {
            var result = await service.UpdateCollectionAsync(id, request);

            if (result.Succeeded)
            {
                return Response<Collection>.Ok(result.Result);
            }

            return result.Fault.ErrorCode switch
            {
                ExceptionCodes.Code404NotFound => Response<Collection>.NotFound(),
                _ => Response<Collection>.BadRequest(result.Fault.ErrorMessage),
            };
        }
        #endregion

        #region DeleteCollectionAsync
        /// <summary>
        /// Deletes a collection.
        /// </summary>
        /// <param name="id">Represents the id of the <see cref="Collection"/>.</param>
        /// <param name="service">A service for <see cref="Collection"/> related operations.</param>
        /// <returns>
        /// A <see cref="Response{Boolean}"/>, containing the details of operation.
        /// </returns>
        [Authorize]
        public async Task<Response<bool>> DeleteCollectionAsync(string id, [Service] CollectionService service)
        {
            var result = await service.DeleteCollectionAsync(id);

            if (result.Succeeded)
            {
                return Response<bool>.NoContent(result.Result);
            }

            return result.Fault.ErrorCode switch
            {
                ExceptionCodes.Code404NotFound => Response<bool>.NotFound(),
                _ => Response<bool>.BadRequest(result.Fault.ErrorMessage),
            };
        }
        #endregion
    }
}
