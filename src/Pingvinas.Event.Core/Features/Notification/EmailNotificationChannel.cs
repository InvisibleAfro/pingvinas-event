using Pingvinas.Event.Core.Features.Notification;
using Pingvinas.Event.Domain.Models;

namespace Pingvinas.Event.Core.Features
{
    public class EmailNotificationChannel : INotificationChannel
    {
        public async Task NotifyParticipantAsync(Participant participant, string message, CancellationToken ct = default)
        {
            try
            {
                await Task.Delay(100, ct);
                // email sending logic here
                return;
            }
            catch (Exception)
            {
                //if transient error, schedule retry later. Log error if not.
            }
        }

        public bool ShouldNotify(Participant participant)
        {
            // Check if participant has a valid email address && is not opted out of email notifications
            return participant.User.Email != null && participant.User.Email.Contains("@");
        }
    }
}
