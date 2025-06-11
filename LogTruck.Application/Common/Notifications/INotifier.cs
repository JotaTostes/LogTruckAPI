using System.Collections.Generic;

namespace LogTruck.Application.Common.Notifications
{
    public interface INotifier
    {
        void Handle(Notification notification);
        List<Notification> GetNotifications();
        bool HasNotification();
        void Clear();
    }
}