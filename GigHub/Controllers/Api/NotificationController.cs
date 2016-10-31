using AutoMapper;
using GigHub.Core.DTOs;
using GigHub.Core.Models;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using GigHub.Core;

namespace GigHub.Controllers.Api
{
    [Authorize]
    public class NotificationController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public NotificationController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<NotificationDto> GetNewNotifications()
        {
            var notifications = _unitOfWork.Notifications
                .GetUnreadNotificationsWithArtist(User.Identity.GetUserId());

            return notifications.Select(Mapper.Map<Notification, NotificationDto>);
        }

        [HttpPost]
        public IHttpActionResult MarkAsRead()
        {
            var userNotifications = _unitOfWork.UserNotifications.GetUnreadUserNotifications(User.Identity.GetUserId());

            foreach (var userNotification in userNotifications)
            {
                userNotification.MarkAsRead();
            }

            _unitOfWork.Complete();

            return Ok();
        }
    }
}
