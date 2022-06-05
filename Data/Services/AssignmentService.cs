using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Todo.Api.Data.Data;
using Todo.Api.Data.Data.Entities;
using Todo.Api.Data.Data.Models.Assignment;
using Todo.Api.Data.Data.Models.Result;
using Todo.Api.Data.Data.Objects;
using Todo.Api.Data.Extensions;

namespace Todo.Api.Data.Services
{
    public class AssignmentService : BaseService
    {
        public AssignmentService(IDbContextFactory<DatabaseContext> dbContextFactory, IHttpContextAccessor contextAccessor) : base(dbContextFactory, contextAccessor)
        {

        }

        #region CreateAssignmentAsync
        /// <summary>
        /// Adds a new <see cref="Assignment"/>.
        /// </summary>
        /// <param name="request">Represents the required data for creating a new <see cref="Assignment"/>.</param>
        /// <returns>
        /// An <see cref="OperationResult{Assignment}"/>, containing the details of operation.
        /// </returns>
        /// <remarks> Produces error codes.
        /// <list type="bullet">
        /// <item><see cref="ExceptionCodes.Code500Problem"/></item>
        /// <item><see cref="ExceptionCodes.Code404NotFound"/></item>
        /// <item><see cref="ExceptionCodes.Code400BadRequest"/></item>
        /// </list>
        /// </remarks>
        public async Task<OperationResult<Assignment>> CreateAssignmentAsync(CreateAssignmentRequest request)
        {
            var validator = new CreateAssignmentValidator();
            var validationResult = validator.Validate(request);

            if (validationResult.IsValid)
            {
                if (await CollectionExistsAsync(request.CollectionId))
                {
                    try
                    {
                        var assignment = new Assignment()
                        {
                            Title = request.Title,
                            Description = request.Description ?? string.Empty,
                            DateScheduled = request.DateScheduled,
                            IsImportant = request.IsImportant ?? false,
                            CollectionId = request.CollectionId,
                            UserId = UserId()
                        };

                        _dbContext.Assignments.Add(assignment);
                        await _dbContext.SaveChangesAsync();

                        return OperationResult<Assignment>.Success(assignment);
                    }
                    catch (Exception)
                    {
                        return OperationResult<Assignment>.Failure(ExceptionCodes.Code500Problem, "Something unexpected occurred.");
                    }
                }

                return OperationResult<Assignment>.Failure(ExceptionCodes.Code404NotFound, "Collection does not exist.");
            }

            return OperationResult<Assignment>.Failure(ExceptionCodes.Code400BadRequest, validationResult.ErrorMessage());
        }
        #endregion

        #region CollectionExistsAsync
        /// <summary>
        /// Checks whether a <see cref="Collection"/> with a certain id exists.
        /// </summary>
        /// <param name="id">Represents the id of the <see cref="Collection"/>.</param>
        /// <returns>
        /// <see langword="true"/> if it does, otherwise <see langword="false"/>
        /// </returns>
        private async Task<bool> CollectionExistsAsync(string id)
        {
            return await _dbContext.Collections.AnyAsync(options => options.Id.Equals(id) && options.UserId.Equals(UserId()));
        }
        #endregion
    }
}
