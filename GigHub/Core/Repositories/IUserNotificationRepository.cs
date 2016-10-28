using GigHub.Core.Models;
using System.Collections.Generic;

namespace GigHub.Core.Repositories
{
    public interface IUserNotificationRepository
    {
        IEnumerable<Notification> GetUnreadNotificationsWithArtist(string userId);
        IEnumerable<UserNotification> GetUnreadUserNotifications(string userId);
    }
}