using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todo.Api.Data.Data.Entities;
using Todo.Api.Data.Data.Models.Collection;
using Todo.Api.Data.Data.Objects;
using Todo.Api.Data.Services;

namespace Todo.Api.Rest.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class CollectionController : ControllerBase
    {
        private readonly CollectionService _service;

        public CollectionController(CollectionService service)
        {
            _service = service;
        }

        #region CreateCollectionAsync
        /// <summary>
        /// Adds a new collection.
        /// </summary>
        /// <param name="request">Represents the required data for creating a new <see cref="Collection"/>.</param>
        /// <returns>
        /// The newly created <see cref="Collection"/> if successful.
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> CreateCollectionAsync(CreateCollectionRequest request)
        {
            var result = await _service.CreateCollectionAsync(request);

            if (result.Succeeded)
            {
                return new ObjectResult(result.Result)
                {
                    StatusCode = 201
                };
            }

            return result.Fault.ErrorCode switch
            {
                ExceptionCodes.Code401Unauthorized => Unauthorized(result.Fault.ErrorMessage),
                _ => BadRequest(result.Fault.ErrorMessage),
            };
        }
        #endregion
    }
}
