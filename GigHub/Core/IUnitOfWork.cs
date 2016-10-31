using GigHub.Core.Repositories;

namespace GigHub.Core
{
    public interface IUnitOfWork
    {
        IAttendanceRepository Attendances { get; }
        IFollowingRepository Followings { get; }
        IGenreRepository Genres { get; }
        IGigRepository Gigs { get; }
        IUserNotificationRepository UserNotifications { get; }
        INotificationRepository Notifications { get; }
        IApplicationUserRepository Users { get; }

        void Complete();
    }
}