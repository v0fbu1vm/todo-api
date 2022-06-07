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

        #region GetAssignmentByIdAsync
        /// <summary>
        /// Gets an <see cref="Assignment"/> by id.
        /// </summary>
        /// <param name="id">Represents the id of the <see cref="Assignment"/>.</param>.
        /// <returns>
        /// An <see cref="Assignment"/> if found.
        /// </returns>
        public async Task<Assignment?> GetAssignmentByIdAsync(string id)
        {
            return Guid.TryParse(id, out _) ? await _dbContext.Assignments.AsNoTracking()
                .FirstOrDefaultAsync(options => options.Id == id && options.UserId == UserId())
                : null;
        }
        #endregion

        #region GetAssignmentsAsync
        /// <summary>
        /// Gets a list of <see cref="Assignment"/>'s. Within a <see cref="Collection"/>.
        /// </summary>
        /// <param name="collectionId">Represents the id of the <see cref="Collection"/>.</param>
        /// <returns>
        /// A list of <see cref="Assignment"/>'s.
        /// </returns>
        public async Task<ICollection<Assignment>> GetAssignmentsAsync(string collectionId)
        {
            return Guid.TryParse(collectionId, out _) ? await _dbContext.Assignments.AsNoTracking()
                .Where(options => options.UserId == UserId() && options.CollectionId == collectionId)
                .ToListAsync()
                : new List<Assignment>();
        }
        #endregion

        #region GetAssignmentsAsync
        /// <summary>
        /// Gets a list of <see cref="Assignment"/>'s.
        /// </summary>
        /// <returns>
        /// A list of <see cref="Assignment"/>'s.
        /// </returns>
        public async Task<ICollection<Assignment>> GetAssignmentsAsync()
        {
            return await _dbContext.Assignments.AsNoTracking()
                .Where(options => options.UserId == UserId())
                .ToListAsync();
        }
        #endregion

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

        #region UpdateAssignmentAsync
        /// <summary>
        /// Updates an <see cref="Assignment"/>.
        /// </summary>
        /// <param name="id">Represents the id of the <see cref="Assignment"/>.</param>
        /// <param name="request">Represents the required data updating an <see cref="Assignment"/>.</param>
        /// <returns>
        /// An <see cref="OperationResult{Assignment}"/>, containing the details of operation.
        /// </returns>
        /// <remarks> Produces error codes.
        /// <list type="bullet">
        /// <item><see cref="ExceptionCodes.Code404NotFound"/></item>
        /// <item><see cref="ExceptionCodes.Code400BadRequest"/></item>
        /// </list>
        /// </remarks>
        public async Task<OperationResult<Assignment>> UpdateAssignmentAsync(string id, UpdateAssignmentRequest request)
        {
            var validator = new UpdateAssignmentValidator();
            var validationResult = validator.Validate(request);

            if (validationResult.IsValid)
            {
                if (Guid.TryParse(id, out _) && id == request.Id)
                {
                    var assignment = await _dbContext.Assignments.FirstOrDefaultAsync(options => options.Id == id && options.UserId == UserId());

                    if(assignment != null)
                    {
                        assignment.Title = request.Title ?? assignment.Title;
                        assignment.Description = request.Description ?? assignment.Description;
                        assignment.DateScheduled = request.DateScheduled ?? assignment.DateScheduled;
                        assignment.IsImportant = request.IsImportant ?? assignment.IsImportant;
                        assignment.IsCompleted = request.IsCompleted ?? assignment.IsCompleted;
                        assignment.DateModified = DateTime.UtcNow;

                        await _dbContext.SaveChangesAsync();

                        return OperationResult<Assignment>.Success(assignment);
                    }

                    return OperationResult<Assignment>.Failure(ExceptionCodes.Code404NotFound, "Not found.");
                }

                return OperationResult<Assignment>.Failure(ExceptionCodes.Code400BadRequest, "Invalid Id.");
            }

            return OperationResult<Assignment>.Failure(ExceptionCodes.Code400BadRequest, validationResult.ErrorMessage());
        }
        #endregion

        #region DeleteAssignmentAsync
        /// <summary>
        /// Deletes an <see cref="Assignment"/>.
        /// </summary>
        /// <param name="id">Represents the id of the <see cref="Assignment"/>.</param>
        /// <returns>
        /// An <see cref="OperationResult{Boolean}"/>, containing the details of operation.
        /// </returns>
        /// <remarks> Produces error codes.
        /// <list type="bullet">
        /// <item><see cref="ExceptionCodes.Code404NotFound"/></item>
        /// <item><see cref="ExceptionCodes.Code400BadRequest"/></item>
        /// </list>
        /// </remarks>
        public async Task<OperationResult<bool>> DeleteAssignmentAsync(string id)
        {
            if (Guid.TryParse(id, out _))
            {
                var assignment = await _dbContext.Assignments.FirstOrDefaultAsync(options => options.Id == id && options.UserId == UserId());

                if (assignment != null)
                {
                    _dbContext.Assignments.Remove(assignment);
                    await _dbContext.SaveChangesAsync();

                    return OperationResult<bool>.Success(true);
                }

                return OperationResult<bool>.Failure(ExceptionCodes.Code404NotFound, "Assignment not found.");
            }

            return OperationResult<bool>.Failure(ExceptionCodes.Code400BadRequest, "Invalid id");
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
