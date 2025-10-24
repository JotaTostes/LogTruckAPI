using LogTruck.Application.Common.Notifications;
using LogTruck.Shared.Responses;
using Microsoft.AspNetCore.Mvc;

namespace LogTruck.API.Controllers
{
    [ApiController]
    public abstract class ApiControllerBase : ControllerBase
    {
        private readonly INotifier _notifier;

        protected ApiControllerBase(INotifier notifier)
        {
            _notifier = notifier;
        }

        protected IActionResult CustomResponse<T>(T? result = default, int statusCode = 200)
        {
            if (!_notifier.HasNotification())
            {
                return StatusCode(statusCode, ApiResponse<T>.SuccessResponse(result, statusCode));
            }

            var errors = _notifier.GetNotifications().Select(n => n.Message);
            return BadRequest(ApiResponse<T>.ErrorResponse(errors));
        }

        protected IActionResult CustomNoContentResponse(int statusCode = 204)
        {
            if (!_notifier.HasNotification())
            {
                return StatusCode(statusCode, ApiResponse.SuccessNoContent(statusCode));
            }

            var errors = _notifier.GetNotifications().Select(n => n.Message);
            return BadRequest(ApiResponse.ErrorResponse(errors));
        }

        protected IActionResult CustomUnauthorizedResponse<T>(T? result = default ,int statusCode = 200)
        {
            if (!_notifier.HasNotification())
            {
                return StatusCode(statusCode, ApiResponse.SuccessNoContent(statusCode));
            }

            var errors = _notifier.GetNotifications().Select(n => n.Message);
            return Unauthorized(ApiResponse.ErrorResponse(errors));
        }


        protected void NotifyError(string message)
        {
            _notifier.Handle("Erro", message);
        }
    }
}