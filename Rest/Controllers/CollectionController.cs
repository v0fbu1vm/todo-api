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

        #region GetCollectionByIdAsync
        /// <summary>
        /// Gets a <see cref="Collection"/> by id.
        /// </summary>
        /// <param name="id">Represents the id of the <see cref="Collection"/>.</param>
        /// <returns>
        /// An <see cref="Microsoft.AspNetCore.Mvc.IActionResult"/>, if found
        /// a <see cref="Collection"/> is imbedded within it.
        /// </returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCollectionByIdAsync(string id)
        {
            var collection = await _service.GetCollectionByIdAsync(id);

            return collection != null ? Ok(collection) : NotFound();
        }
        #endregion

        #region GetCollectionByNameAsync
        /// <summary>
        /// Gets a <see cref="Collection"/> by name.
        /// </summary>
        /// <param name="name">Represents the name of the <see cref="Collection"/>.</param>
        /// <returns>
        /// An <see cref="Microsoft.AspNetCore.Mvc.IActionResult"/>, if found
        /// a <see cref="Collection"/> is imbedded within it.
        /// </returns>
        [HttpGet("{name}")]
        public async Task<IActionResult> GetCollectionByNameAsync(string name)
        {
            var collection = await _service.GetCollectionByNameAsync(name);

            return collection != null ? Ok(collection) : NotFound();
        }
        #endregion

        #region GetCollectionsAsync
        /// <summary>
        /// Gets a list of <see cref="Collection"/>.
        /// </summary>
        /// <returns>
        /// An <see cref="Microsoft.AspNetCore.Mvc.IActionResult"/>, with
        /// a list of <see cref="Collection"/>'s imbedded within it.
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> GetCollectionsAsync()
        {
            return Ok(await _service.GetCollectionsAsync());
        }
        #endregion

        #region CreateCollectionAsync
        /// <summary>
        /// Adds a new <see cref="Collection"/>.
        /// </summary>
        /// <param name="request">Represents the required data for creating a new <see cref="Collection"/>.</param>
        /// <returns>
        /// An <see cref="Microsoft.AspNetCore.Mvc.IActionResult"/>, if successful
        /// the newly created <see cref="Collection"/> is imbedded within it.
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> CreateCollectionAsync([FromBody] CreateCollectionRequest request)
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
                ExceptionCodes.Code500Problem => Problem(result.Fault.ErrorMessage),
                _ => BadRequest(result.Fault.ErrorMessage),
            };
        }
        #endregion

        #region UpdateCollectionAsync
        /// <summary>
        /// Updates a <see cref="Collection"/>.
        /// </summary>
        /// <param name="id">Represents the id of the <see cref="Collection"/>.</param>
        /// <param name="request">Represents the required data for updating a <see cref="Collection"/>.</param>
        /// <returns>
        /// An <see cref="Microsoft.AspNetCore.Mvc.IActionResult"/>, if successful
        /// the newly updated <see cref="Collection"/> is imbedded within it.
        /// </returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCollectionAsync(string id, [FromBody] UpdateCollectionRequest request)
        {
            var result = await _service.UpdateCollectionAsync(id, request);

            if (result.Succeeded)
            {
                return Ok(result.Result);
            }

            return result.Fault.ErrorCode switch
            {
                ExceptionCodes.Code404NotFound => NotFound(result.Fault.ErrorMessage),
                _ => BadRequest(result.Fault.ErrorMessage),
            };
        }
        #endregion

        #region DeleteCollectionAsync
        /// <summary>
        /// Deletes a <see cref="Collection"/>.
        /// </summary>
        /// <param name="id">Represents the id of the <see cref="Collection"/>.</param>
        /// <returns>
        /// An <see cref="Microsoft.AspNetCore.Mvc.IActionResult"/>, if successful
        /// a <see cref="NoContentResult"/> is returned.
        /// </returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCollectionAsync(string id)
        {
            var result = await _service.DeleteCollectionAsync(id);

            if (result.Succeeded)
            {
                return NoContent();
            }

            return result.Fault.ErrorCode switch
            {
                ExceptionCodes.Code404NotFound => NotFound(result.Fault.ErrorMessage),
                _ => BadRequest(result.Fault.ErrorMessage),
            };
        }
        #endregion
    }
}
