using GigHub.Core.Models;
using System;

namespace GigHub.Core.DTOs
{
    public class NotificationDto
    {
        public DateTime DateTime { get; set; }

        public NotificationType NotificationType { get; set; }

        public DateTime? OriginalDateTime { get; set; }

        public string OriginalVenue { get; set; }

        public GigDto Gig { get; set; }
    }
}