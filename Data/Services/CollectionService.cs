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

        #region CreateCollectionAsync
        /// <summary>
        /// Adds a new collection.
        /// </summary>
        /// <param name="request">Represents the required data for creating a new <see cref="Collection"/>.</param>
        /// <returns>
        /// An <see cref="OperationResult{Collection}"/>, containing the details of operation.
        /// </returns>
        /// <remarks> Produces error codes.
        /// <list type="table">
        /// <item><see cref="ExceptionCodes.Code401Unauthorized"/></item>
        /// <item><see cref="ExceptionCodes.Code400BadRequest"/></item>
        /// </list>
        /// </remarks>
        public async Task<OperationResult<Collection>> CreateCollectionAsync(CreateCollectionRequest request)
        {
            var validator = new CreateCollectionValidator();
            var validationResult = validator.Validate(request);

            if (validationResult.IsValid)
            {
                if(await NameExistsAsync(request.Name) is false)
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
                        return OperationResult<Collection>.Failure(ExceptionCodes.Code401Unauthorized, "Unable to identify user.");
                    }
                }

                return OperationResult<Collection>.Failure(ExceptionCodes.Code400BadRequest, "Name already exists.");
            }

            return OperationResult<Collection>.Failure(ExceptionCodes.Code400BadRequest, validationResult.ErrorMessage());
        }
        #endregion

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
        #endregion
    }
}
