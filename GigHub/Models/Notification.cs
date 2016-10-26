using System;
using System.ComponentModel.DataAnnotations;

namespace GigHub.Models
{
    public class Notification
    {
        public int Id { get; private set; }

        public DateTime DateTime { get; private set; }

        public NotificationType NotificationType { get; private set; }

        public DateTime? OriginalDateTime { get; private set; }

        public string OriginalValue { get; private set; }

        [Required]
        public Gig Gig { get; private set; }

        private Notification(Gig gig, NotificationType type)
        {
            if (gig == null)
                throw new ArgumentNullException(nameof(gig));

            Gig = gig;
            NotificationType = type;
            DateTime = DateTime.Now;
        }

        //used by EF
        private Notification()
        {
        }

        public static Notification GetNotificationForCreatedGig(Gig gig)
        {
            return new Notification(gig, NotificationType.GigCreated);
        }

        public static Notification GetNotificationForUpdatedGig(Gig updatedGig, DateTime originalDateTime, string originalVenue)
        {
            var notification = new Notification(updatedGig, NotificationType.GigUpdated);
            notification.OriginalDateTime = originalDateTime;
            notification.OriginalValue = originalVenue;

            return notification;
        }

        public static Notification GetNotificationForCanceledGig(Gig gig)
        {
            return new Notification(gig, NotificationType.GigCanceled);
        }
    }
}