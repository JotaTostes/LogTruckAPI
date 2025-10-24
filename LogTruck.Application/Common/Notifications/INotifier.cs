using System.Collections.Generic;

namespace LogTruck.Application.Common.Notifications
{
    public interface INotifier
    {
        void Handle(string key, string message);
        List<Notification> GetNotifications();
        bool HasNotification();
        void Clear();
    }
}