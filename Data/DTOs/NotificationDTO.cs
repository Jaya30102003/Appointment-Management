using System;
using Notifications.Model;

namespace Notifications.DTO;

public class NotificationDTO
{
    public Guid NotificationId { get; set; }
    public Guid AppointmentId { get; set; }
    public string NotificationTitle { get; set; }
    public string NotificationMessage { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid RecipientId { get; set; }

    public RecipientType Recipient { get; set; }
}
