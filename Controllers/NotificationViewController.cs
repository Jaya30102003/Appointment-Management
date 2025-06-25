using Microsoft.AspNetCore.Mvc;
using Notifications.Service;
using Notifications.DTO;

namespace Notifications.ViewControllers 
{
    [Route("NotificationView")]
    public class NotificationViewController : Controller
    {
        private readonly INotificationService _service;

        public NotificationViewController(INotificationService service)
        {
            _service = service;
        }

        // GET: /NotificationView/Doctor/abc123
        [HttpGet("{recipient}/{recipientId}")]
        public async Task<IActionResult> ViewByRecipientAndId(string recipient, string recipientId)
        {
            if (string.IsNullOrWhiteSpace(recipient) || string.IsNullOrWhiteSpace(recipientId))
            {
                ViewData["Error"] = "Please select a recipient and enter a valid ID.";
                return View("Notifications", Enumerable.Empty<NotificationDTO>());
            }

            var notifications = await _service.GetByRecipientAndIdAsync(recipient, recipientId);
            return View("Notifications", notifications);
        }

        // GET: /NotificationView/Form
        [HttpGet("Form")]
        public IActionResult Form()
        {
            return View("Form", null);
        }

        // POST: /NotificationView/Form
        [HttpPost("Form")]
        public async Task<IActionResult> Form(string recipient, string recipientId)
        {
            if (string.IsNullOrWhiteSpace(recipient) || string.IsNullOrWhiteSpace(recipientId))
            {
                ViewData["Error"] = "Recipient and ID are required.";
                return View("Form", null);
            }

            var notifications = await _service.GetByRecipientAndIdAsync(recipient.Trim(), recipientId.Trim());
            return View("Form", notifications);
        }

        // GET: /NotificationView/All
        [HttpGet("All")]
        public async Task<IActionResult> AllNotifications()
        {
            var allNotifications = await _service.GetAll();
            return View("Notifications", allNotifications);
        }

        // GET: /NotificationView/Selector
        [HttpGet("Selector")]
        public IActionResult Selector()
        {
            return View("Selector");
        }
    }
}
