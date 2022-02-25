using System.Collections.Generic;
using System.Linq;

namespace Comparis.CrossCutting.Notification
{
    public class MessageManager : IMessageManager
    {
        private List<Notification> _notifications;

        public MessageManager()
        {
            _notifications = new List<Notification>();
        }

        public virtual List<Notification> GetNotifications()
        {
            return _notifications;
        }

        public virtual bool HasNotifications()
        {
            return _notifications.Any();
        }

        public void Add(Notification message)
        {
            _notifications.Add(message);
        }
    }
}