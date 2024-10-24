using Application.Common.Helpers.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace PeopleRegistration.Service.Controllers.Base
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected IActionResult HandleResponse<T>(Func<T> func, string successMessage = null)
        {
            try
            {
                T data = func();

                return CreateResponse(data, successMessage);
            }
            catch (BusinessException)
            {
                throw;
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        #region Private Method
        private ObjectResult CreateResponse<T>(T data, string successMessage)
        {
            int statusCode = StatusCodes.Status200OK;

            if (data is null)
            {
                statusCode = StatusCodes.Status400BadRequest;
                return StatusCode(statusCode, new ApiResponse<T>
                {
                    Success = false,
                    Message = successMessage ?? "Error processing the request",
                    StatusCode = statusCode,
                    Data = data
                });
            }

            return StatusCode(statusCode, new ApiResponse<T>
            {
                Success = true,
                Message = successMessage ?? "Request processed successfully",
                StatusCode = statusCode,
                Data = data
            });
        }

        private ObjectResult HandleException(Exception ex)
        {
            int statusCode;
            string errorMessage;

            switch (ex)
            {
                case ArgumentNullException:
                    statusCode = StatusCodes.Status400BadRequest;
                    errorMessage = "Bad request - Argument was null.";
                    break;

                case UnauthorizedAccessException:
                    statusCode = StatusCodes.Status401Unauthorized;
                    errorMessage = "Unauthorized access.";
                    break;

                case KeyNotFoundException:
                    statusCode = StatusCodes.Status404NotFound;
                    errorMessage = "Resource not found.";
                    break;

                default:
                    statusCode = StatusCodes.Status500InternalServerError;
                    errorMessage = "An internal server error occurred.";
                    break;
            }

            return StatusCode(statusCode, new ApiResponse<string>
            {
                Success = false,
                Message = $"{errorMessage} Details: {ex.Message}",
                StatusCode = statusCode,
                Data = null
            });
        }

        #endregion Private Method
    }
}