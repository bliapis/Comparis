using System.Collections.Generic;

namespace Comparis.CrossCutting.Notification
{
    public interface IMessageManager
    {
        public bool HasNotifications();
        public List<Notification> GetNotifications();

        public void Add(Notification notification);
    }
}