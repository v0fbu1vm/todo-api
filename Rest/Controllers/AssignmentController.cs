using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todo.Api.Data.Data.Entities;
using Todo.Api.Data.Data.Models.Assignment;
using Todo.Api.Data.Data.Objects;
using Todo.Api.Data.Services;

namespace Todo.Api.Rest.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class AssignmentController : ControllerBase
    {
        private readonly AssignmentService _service;

        public AssignmentController(AssignmentService service)
        {
            _service = service;
        }

        #region GetAssignmentByIdAsync
        /// <summary>
        /// Gets an <see cref="Assignment"/> by id.
        /// </summary>
        /// <param name="id">Represents the id of the <see cref="Assignment"/>.</param>.
        /// <returns>
        /// An <see cref="Microsoft.AspNetCore.Mvc.IActionResult"/>, an
        /// <see cref="Assignment"/> is imbedded within it if found.
        /// </returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAssignmentByIdAsync(string id)
        {
            var assignment = await _service.GetAssignmentByIdAsync(id);

            return assignment != null ? Ok(assignment) : NotFound();
        }
        #endregion

        #region GetAssignmentsAsync
        /// <summary>
        /// Gets a list of <see cref="Assignment"/>'s. Within a <see cref="Collection"/>.
        /// </summary>
        /// <param name="collectionId">Represents the id of the <see cref="Collection"/>.</param>
        /// <returns>
        /// An <see cref="Microsoft.AspNetCore.Mvc.IActionResult"/>, with
        /// a list of <see cref="Assignment"/>'s imbedded within it.
        /// </returns>
        [HttpGet("{collectionId}")]
        public async Task<IActionResult> GetAssignmentsAsync(string collectionId)
        {
            return Ok(await _service.GetAssignmentsAsync(collectionId));
        }
        #endregion

        #region GetAssignmentsAsync
        /// <summary>
        /// Gets a list of <see cref="Assignment"/>.
        /// </summary>
        /// <returns>
        /// An <see cref="Microsoft.AspNetCore.Mvc.IActionResult"/>, with
        /// a list of <see cref="Assignment"/>'s imbedded within it.
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> GetAssignmentsAsync()
        {
            return Ok(await _service.GetAssignmentsAsync());
        }
        #endregion

        #region CreateAssignmentAsync
        /// <summary>
        /// Adds a new <see cref="Assignment"/>.
        /// </summary>
        /// <param name="request">Represents the required data for creating a new <see cref="Assignment"/>.</param>
        /// <returns>
        /// An <see cref="Microsoft.AspNetCore.Mvc.IActionResult"/>, if successful
        /// the newly created <see cref="Assignment"/> is imbedded within it.
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> CreateAssignmentAsync([FromBody] CreateAssignmentRequest request)
        {
            var result = await _service.CreateAssignmentAsync(request);

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
                ExceptionCodes.Code404NotFound => NotFound(result.Fault.ErrorMessage),
                _ => BadRequest(result.Fault.ErrorMessage),
            };
        }
        #endregion

        #region UpdateAssignmentAsync
        /// <summary>
        /// Updates an <see cref="Assignment"/>.
        /// </summary>
        /// <param name="id">Represents the id of the <see cref="Assignment"/>.</param>
        /// <param name="request">Represents the required data for updating an <see cref="Assignment"/>.</param>
        /// <returns>
        /// An <see cref="Microsoft.AspNetCore.Mvc.IActionResult"/>, if successful
        /// the newly updated <see cref="Assignment"/> is imbedded within it.
        /// </returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCollectionAsync(string id, [FromBody] UpdateAssignmentRequest request)
        {
            var result = await _service.UpdateAssignmentAsync(id, request);

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

        #region DeleteAssignmentAsync
        /// <summary>
        /// Deletes an <see cref="Assignment"/>.
        /// </summary>
        /// <param name="id">Represents the id of the <see cref="Assignment"/>.</param>
        /// <returns>
        /// An <see cref="Microsoft.AspNetCore.Mvc.IActionResult"/>, if successful
        /// a <see cref="NoContentResult"/> is returned.
        /// </returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAssignmentAsync(string id)
        {
            var result = await _service.DeleteAssignmentAsync(id);

            if(result.Succeeded)
            {
                return NoContent();
            }

            return result.Fault.ErrorCode switch
            {
                ExceptionCodes.Code404NotFound => NotFound(result.Fault.ErrorMessage),
                _ => BadRequest(result.Fault.ErrorMessage)
            }; 
        }
        #endregion
    }
}
