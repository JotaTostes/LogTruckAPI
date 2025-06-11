using System.Collections.Generic;
using System.Linq;

namespace LogTruck.Application.Common.Notifications
{
    public class Notifier : INotifier
    {
        private readonly List<Notification> _notifications = new();

        public void Handle(Notification notification)
        {
            _notifications.Add(notification);
        }

        public List<Notification> GetNotifications() => _notifications;

        public bool HasNotification() => _notifications.Any();

        public void Clear() => _notifications.Clear();
    }
}