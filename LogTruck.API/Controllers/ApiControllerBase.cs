using LogTruck.Application.Common.Notifications;
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

        protected ActionResult CustomResponse(object? result = null)
        {
            if (!_notifier.HasNotification())
                return Ok(result);

            return BadRequest(new
            {
                errors = _notifier.GetNotifications().Select(n => n.Message)
            });
        }

        protected void NotifyError(string key, string message)
        {
            _notifier.Handle(new Notification(key, message));
        }
    }
}