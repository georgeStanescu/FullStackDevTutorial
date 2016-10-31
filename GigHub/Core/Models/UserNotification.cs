using System;

namespace GigHub.Core.Models
{
    public class UserNotification
    {
        public UserNotification(ApplicationUser user, Notification notification)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            if (notification == null)
                throw new ArgumentNullException(nameof(notification));

            Notification = notification;
            NotificationId = notification.Id;
            User = user;
            UserId = user.Id;
        }

        //declared only for EF
        protected UserNotification()
        {
        }

        public string UserId { get; private set; }

        public int NotificationId { get; private set; }

        public ApplicationUser User { get; private set; }

        public Notification Notification { get; private set; }

        public bool IsRead { get; private set; }

        public void MarkAsRead() {
            IsRead = true;
        }
    }
}