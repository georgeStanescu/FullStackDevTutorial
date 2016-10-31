using System.Linq;
using FluentAssertions;
using GigHub.Core.Models;
using GigHub.Persistence;
using GigHub.Persistence.Repositories;
using GigHub.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Data.Entity;

namespace GigHub.Tests.Persistence.Repositories
{
    [TestClass]
    public class NotificationRepositoryTests
    {
        private const string _userId = "1";
        private NotificationRepository _repository;
        private Mock<DbSet<UserNotification>> _mockNotifications;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockNotifications = new Mock<DbSet<UserNotification>>();

            var mockContext = new Mock<IApplicationDbContext>();
            mockContext.SetupGet(c => c.UserNotifications).Returns(_mockNotifications.Object);


            _repository = new NotificationRepository(mockContext.Object);
        }


        [TestMethod]
        public void GetUnreadNotificationsWithArtist_NotificationIsRead_ShouldNotBeReturned()
        {
            var notification = Notification.GetNotificationForCanceledGig(new Gig());
            var user = new ApplicationUser { Id = _userId };
            var userNotification = new UserNotification(user, notification);
            userNotification.MarkAsRead();

            _mockNotifications.SetSource(new[] { userNotification });

            var notifications = _repository.GetUnreadNotificationsWithArtist(user.Id);

            notifications.Should().BeEmpty();
        }

        [TestMethod]
        public void GetUnreadNotificationsWithArtist_NotificationIsForADifferentUser_ShouldNotBeReturned()
        {
            var notification = Notification.GetNotificationForCanceledGig(new Gig());
            var user = new ApplicationUser { Id = _userId };
            var userNotification = new UserNotification(user, notification);

            _mockNotifications.SetSource(new[] { userNotification });

            var notifications = _repository.GetUnreadNotificationsWithArtist(user.Id + "-");

            notifications.Should().BeEmpty();
        }

        [TestMethod]
        public void GetUnreadNotificationsWithArtist_NewNotificationForTheGivenUser_ShouldBeReturned()
        {
            var notification = Notification.GetNotificationForCanceledGig(new Gig());
            var user = new ApplicationUser { Id = _userId };
            var userNotification = new UserNotification(user, notification);

            _mockNotifications.SetSource(new[] { userNotification });

            var notifications = _repository.GetUnreadNotificationsWithArtist(user.Id);

            notifications.Should().HaveCount(1);
            notifications.First().Should().Be(notification);
        }
    }
}
