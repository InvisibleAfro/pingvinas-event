using Pingvinas.Event.Domain.Models;

namespace Pingvinas.Event.Core.Features.Notification
{
    public class SmsNotificationChannel : INotificationChannel
    {
        public async Task NotifyParticipantAsync(Participant participant, string message, CancellationToken ct = default)
        {
            try
            {
                await Task.Delay(30, ct);
                // Simulate sending an SMS notification
            }
            catch (Exception)
            {
                //try later if transient. Log error if not transiet.
            }
        }

        public bool ShouldNotify(Participant participant)
        {
            //check if phone number is valid and user has opted in for SMS notifications
            return true;
        }
    }
}
