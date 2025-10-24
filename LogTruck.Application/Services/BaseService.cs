using LogTruck.Application.Common.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogTruck.Application.Services
{
    public abstract class BaseService
    {
        protected readonly INotifier _notifier;

        protected BaseService(INotifier notifier)
        {
            _notifier = notifier;
        }

        protected void Notify(string key, string message)
        {
            _notifier.Handle(key, message);
        }

        protected void NotifyError(string message)
        {
            _notifier.Handle("Erro", message);
        }

        protected void NotifyValidation(string campo, string message)
        {
            _notifier.Handle($"Validacao.{campo}", message);
        }
    }
}
