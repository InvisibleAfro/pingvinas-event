using Pingvinas.Event.Domain.Models;

namespace Pingvinas.Event.Core.Features.Notification
{
    public class CompositeNotificationService(IEnumerable<INotificationChannel> channels) : INotificationService
    {
        public async Task NotifyParticipantAsync(Participant participant, string message)
        {
            foreach (var channel in channels)
            {
                if (channel.ShouldNotify(participant))
                {
                    await channel.NotifyParticipantAsync(participant, message);
                }
            }
        }
    }
}
