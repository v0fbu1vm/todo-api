using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Todo.Api.Data.Data;
using Todo.Api.Data.Data.Entities;
using Todo.Api.Data.Data.Models.Collection;
using Todo.Api.Data.Data.Models.Result;
using Todo.Api.Data.Data.Objects;
using Todo.Api.Data.Extensions;

namespace Todo.Api.Data.Services
{
    public class CollectionService : BaseService
    {
        public CollectionService(IDbContextFactory<DatabaseContext> dbContextFactory, IHttpContextAccessor contextAccessor) : base(dbContextFactory, contextAccessor)
        {
        }

        #region GetCollectionByIdAsync

        /// <summary>
        /// Gets a <see cref="Collection"/> by id.
        /// </summary>
        /// <param name="id">Represents the id of the <see cref="Collection"/>.</param>.
        /// <returns>
        /// A <see cref="Collection"/> if found.
        /// </returns>
        public async Task<Collection?> GetCollectionByIdAsync(string id)
        {
            return Guid.TryParse(id, out _) ? await _dbContext.Collections.AsNoTracking()
                .FirstOrDefaultAsync(options => options.Id == id && options.UserId == UserId())
                : null;
        }

        #endregion GetCollectionByIdAsync

        #region GetCollectionByNameAsync

        /// <summary>
        /// Gets a <see cref="Collection"/> by name.
        /// </summary>
        /// <param name="id">Represents the name of the <see cref="Collection"/>.</param>.
        /// <returns>
        /// A <see cref="Collection"/> if found.
        /// </returns>
        public async Task<Collection?> GetCollectionByNameAsync(string name)
        {
            return string.IsNullOrEmpty(name) is false ? await _dbContext.Collections.AsNoTracking()
                .FirstOrDefaultAsync(options => options.Name == name && options.UserId == UserId())
                : null;
        }

        #endregion GetCollectionByNameAsync

        #region GetCollectionsAsync

        /// <summary>
        /// Gets a list of <see cref="Collection"/>.
        /// </summary>
        /// <returns>
        /// A list of <see cref="Collection"/>'s.
        /// </returns>
        public async Task<ICollection<Collection>> GetCollectionsAsync()
        {
            return await _dbContext.Collections.AsNoTracking()
                .Where(options => options.UserId == UserId())
                .ToListAsync();
        }

        #endregion GetCollectionsAsync

        #region CreateCollectionAsync

        /// <summary>
        /// Adds a new <see cref="Collection"/>.
        /// </summary>
        /// <param name="request">Represents the required data for creating a new <see cref="Collection"/>.</param>
        /// <returns>
        /// An <see cref="OperationResult{Collection}"/>, containing the details of operation.
        /// </returns>
        /// <remarks> Produces error codes.
        /// <list type="bullet">
        /// <item><see cref="ExceptionCodes.Code500Problem"/></item>
        /// <item><see cref="ExceptionCodes.Code400BadRequest"/></item>
        /// </list>
        /// </remarks>
        public async Task<OperationResult<Collection>> CreateCollectionAsync(CreateCollectionRequest request)
        {
            var validator = new CreateCollectionValidator();
            var validationResult = validator.Validate(request);

            if (validationResult.IsValid)
            {
                if (await NameExistsAsync(request.Name) is false)
                {
                    try
                    {
                        var collection = new Collection()
                        {
                            Name = request.Name,
                            UserId = UserId()
                        };

                        _dbContext.Collections.Add(collection);
                        await _dbContext.SaveChangesAsync();

                        return OperationResult<Collection>.Success(collection);
                    }
                    catch (Exception)
                    {
                        return OperationResult<Collection>.Failure(ExceptionCodes.Code500Problem, "Something unexpected occurred.");
                    }
                }

                return OperationResult<Collection>.Failure(ExceptionCodes.Code400BadRequest, "Name already exists.");
            }

            return OperationResult<Collection>.Failure(ExceptionCodes.Code400BadRequest, validationResult.ErrorMessage());
        }

        #endregion CreateCollectionAsync

        #region UpdateCollectionAsync

        /// <summary>
        /// Updates a <see cref="Collection"/>.
        /// </summary>
        /// <param name="id">Represents the id of the <see cref="Collection"/>.</param>
        /// <param name="request">Represents the required data updating a <see cref="Collection"/>.</param>
        /// <returns>
        /// An <see cref="OperationResult{Collection}"/>, containing the details of operation.
        /// </returns>
        /// <remarks> Produces error codes.
        /// <list type="bullet">
        /// <item><see cref="ExceptionCodes.Code404NotFound"/></item>
        /// <item><see cref="ExceptionCodes.Code400BadRequest"/></item>
        /// </list>
        /// </remarks>
        public async Task<OperationResult<Collection>> UpdateCollectionAsync(string id, UpdateCollectionRequest request)
        {
            var validator = new UpdateCollectionValidator();
            var validationResult = validator.Validate(request);

            if (validationResult.IsValid)
            {
                if (Guid.TryParse(id, out _) && id == request.Id)
                {
                    var collection = await _dbContext.Collections.FirstOrDefaultAsync(options => options.Id == id && options.UserId == UserId());

                    if (collection != null)
                    {
                        if (await NameExistsAsync(request.Name) is false)
                        {
                            collection.Name = request.Name;
                            collection.DateModified = DateTime.UtcNow;

                            await _dbContext.SaveChangesAsync();

                            return OperationResult<Collection>.Success(collection);
                        }

                        return OperationResult<Collection>.Failure(ExceptionCodes.Code400BadRequest, "Name already exists.");
                    }

                    return OperationResult<Collection>.Failure(ExceptionCodes.Code404NotFound, "Not found.");
                }

                return OperationResult<Collection>.Failure(ExceptionCodes.Code400BadRequest, "Invalid Id.");
            }

            return OperationResult<Collection>.Failure(ExceptionCodes.Code400BadRequest, validationResult.ErrorMessage());
        }

        #endregion UpdateCollectionAsync

        #region DeleteCollectionAsync

        /// <summary>
        /// Deletes a <see cref="Collection"/>.
        /// </summary>
        /// <param name="id">Represents the id of the <see cref="Collection"/>.</param>
        /// <returns>
        /// An <see cref="OperationResult{Boolean}"/>, containing the details of operation.
        /// </returns>
        /// <remarks> Produces error codes.
        /// <list type="bullet">
        /// <item><see cref="ExceptionCodes.Code404NotFound"/></item>
        /// <item><see cref="ExceptionCodes.Code400BadRequest"/></item>
        /// </list>
        /// </remarks>
        public async Task<OperationResult<bool>> DeleteCollectionAsync(string id)
        {
            if (Guid.TryParse(id, out _))
            {
                var collection = await _dbContext.Collections.FirstOrDefaultAsync(options => options.Id == id && options.UserId == UserId());

                if (collection != null)
                {
                    _dbContext.Collections.Remove(collection);
                    await _dbContext.SaveChangesAsync();

                    return OperationResult<bool>.Success(true);
                }

                return OperationResult<bool>.Failure(ExceptionCodes.Code404NotFound, "Collection not found.");
            }

            return OperationResult<bool>.Failure(ExceptionCodes.Code400BadRequest, "Invalid id");
        }

        #endregion DeleteCollectionAsync

        #region NameExistsAsync

        /// <summary>
        /// Checks whether a <see cref="Collection"/> with a certain name exists.
        /// </summary>
        /// <param name="name">Represents the name of a <see cref="Collection"/>.</param>
        /// <returns>
        /// <see langword="true"/> if it does, otherwise <see langword="false"/>
        /// </returns>
        private async Task<bool> NameExistsAsync(string name)
        {
            return await _dbContext.Collections.AnyAsync(options => options.Name.Equals(name) && options.UserId.Equals(UserId()));
        }

        #endregion NameExistsAsync
    }
}