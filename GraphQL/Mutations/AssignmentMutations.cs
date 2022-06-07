using HotChocolate.AspNetCore.Authorization;
using Todo.Api.Data.Data.Entities;
using Todo.Api.Data.Data.Models.Assignment;
using Todo.Api.Data.Data.Models.Response;
using Todo.Api.Data.Data.Objects;
using Todo.Api.Data.Services;

namespace Todo.Api.GraphQL.Mutations
{
    [ExtendObjectType(OperationTypeNames.Mutation)]
    public class AssignmentMutations
    {
        #region CreateAssignmentAsync
        /// <summary>
        /// Adds a new <see cref="Assignment"/>.
        /// </summary>
        /// <param name="request">Represents the required data for creating a new <see cref="Assignment"/>.</param>
        /// <param name="service">A service for <see cref="Assignment"/> related operations.</param>
        /// <returns>
        /// A <see cref="Response{Assignment}"/>, containing the details of operation.
        /// </returns>
        [Authorize]
        public async Task<Response<Assignment>> CreateAssignmentAsync(CreateAssignmentRequest request, [Service] AssignmentService service)
        {
            var result = await service.CreateAssignmentAsync(request);

            if (result.Succeeded)
            {
                return Response<Assignment>.Created(result.Result);
            }

            return result.Fault.ErrorCode switch
            {
                ExceptionCodes.Code500Problem => Response<Assignment>.Problem(result.Fault.ErrorMessage),
                ExceptionCodes.Code404NotFound => Response<Assignment>.NotFound(result.Fault.ErrorMessage),
                _ => Response<Assignment>.BadRequest(result.Fault.ErrorMessage),
            };
        }
        #endregion

        #region UpdateAssignmentAsync
        /// <summary>
        /// Updates an <see cref="Assignment"/>.
        /// </summary>
        /// <param name="id">Represents the id of the <see cref="Assignment"/>.</param>
        /// <param name="request">Represents the required data for updating an <see cref="Assignment"/>.</param>
        /// <param name="service">A service for <see cref="Assignment"/> related operations.</param>
        /// <returns>
        /// A <see cref="Response{Assignment}"/>, containing the details of operation.
        /// </returns>
        [Authorize]
        public async Task<Response<Assignment>> UpdateAssignmentAsync(string id, UpdateAssignmentRequest request, [Service] AssignmentService service)
        {
            var result = await service.UpdateAssignmentAsync(id, request);

            if (result.Succeeded)
            {
                return Response<Assignment>.Ok(result.Result);
            }

            return result.Fault.ErrorCode switch
            {
                ExceptionCodes.Code404NotFound => Response<Assignment>.NotFound(),
                _ => Response<Assignment>.BadRequest(result.Fault.ErrorMessage),
            };
        }
        #endregion

        #region DeleteAssignmentAsync
        /// <summary>
        /// Deletes an <see cref="Assignment"/>.
        /// </summary>
        /// <param name="id">Represents the id of the <see cref="Assignment"/>.</param>
        /// <param name="service">A service for <see cref="Assignment"/> related operations.</param>
        /// <returns>
        /// A <see cref="Response{Boolean}"/>, containing the details of operation.
        /// </returns>
        [Authorize]
        public async Task<Response<bool>> DeleteAssignmentAsync(string id, [Service] AssignmentService service)
        {
            var result = await service.DeleteAssignmentAsync(id);

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
