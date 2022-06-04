using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Todo.Api.Data.Data.Entities;
using Todo.Api.Data.Data.Models.Login;
using Todo.Api.Data.Data.Models.Register;
using Todo.Api.Data.Data.Models.Result;
using Todo.Api.Data.Data.Models.Token;
using Todo.Api.Data.Extensions;
using Todo.Api.Data.Helpers;

namespace Todo.Api.Data.Services
{
    /// <summary>
    /// A service for authentication related operations. 
    /// </summary>
    public class AuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly TokenProvider _tokenProvider;

        public AuthService(UserManager<User> userManager, SignInManager<User> signInManager, TokenProvider tokenProvider)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenProvider = tokenProvider;
        }

        #region SignInAsync
        /// <summary>
        /// Used for signing a user in.
        /// </summary>
        /// <param name="request">Represents the required data for signing a user in.</param>
        /// <returns>
        /// An <see cref="OperationResult{Token}"/>, containing the details of operation.
        /// </returns>
        /// <remarks> Produces error codes.
        /// <list type="table">
        /// <item><see cref="StatusCodes.Status404NotFound"/></item>
        /// <item><see cref="StatusCodes.Status400BadRequest"/></item>
        /// </list>
        /// </remarks>
        public async Task<OperationResult<Token>> SignInAsync(LoginRequest request)
        {
            var validator = new LoginValidator();
            var validationResult = validator.Validate(request);

            if (validationResult.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(request.Email);

                if (user != null)
                {
                    var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

                    if (result.Succeeded)
                    {
                        return OperationResult<Token>.IsSuccess(_tokenProvider.GenerateToken(user));
                    }

                    return OperationResult<Token>.IsFailure(StatusCodes.Status400BadRequest, "Authentication failed.");
                }

                return OperationResult<Token>.IsFailure(StatusCodes.Status404NotFound, "User not found.");
            }

            return OperationResult<Token>.IsFailure(StatusCodes.Status400BadRequest, validationResult.ErrorMessage());
        }
        #endregion

        #region RegisterAsync
        /// <summary>
        /// Used for registering a user.
        /// </summary>
        /// <param name="request">Represents the required data for registering a user.</param>
        /// <returns>
        /// An <see cref="OperationResult{Token}"/>, containing the details of operation.
        /// </returns>
        /// <remarks> Produces error codes.
        /// <list type="table">
        /// <item><see cref="StatusCodes.Status400BadRequest"/></item>
        /// </list>
        /// </remarks>
        public async Task<OperationResult<Token>> RegisterAsync(RegisterRequest request)
        {
            var validator = new RegisterValidator();
            var validationResult = validator.Validate(request);

            if (validationResult.IsValid)
            {
                var newUser = new User()
                {
                    Name = request.Name,
                    Email = request.Email,
                    UserName = request.Email.RemoveSpecialCharacters()
                };

                var result = await _userManager.CreateAsync(newUser, request.Password);

                if (result.Succeeded)
                {
                    var user = await _userManager.FindByEmailAsync(request.Email);

                    return OperationResult<Token>.IsSuccess(_tokenProvider.GenerateToken(user));
                }

                return OperationResult<Token>.IsFailure(StatusCodes.Status400BadRequest, result.ErrorMessage());
            }

            return OperationResult<Token>.IsFailure(StatusCodes.Status400BadRequest, validationResult.ErrorMessage());
        }
        #endregion
    }
}
