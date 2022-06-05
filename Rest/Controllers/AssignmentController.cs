using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todo.Api.Data.Data.Entities;
using Todo.Api.Data.Data.Models.Assignment;
using Todo.Api.Data.Data.Objects;
using Todo.Api.Data.Services;

namespace Todo.Api.Rest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AssignmentController : ControllerBase
    {
        private readonly AssignmentService _service;
        
        public AssignmentController(AssignmentService service)
        {
            _service = service;
        }

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
    }
}
