using Pingvinas.Event.Domain.Models;

namespace Pingvinas.Event.Core.Features.Notification
{
    public interface INotificationChannel
    {
        Task NotifyParticipantAsync(Participant participant, string message, CancellationToken ct = default);

        bool ShouldNotify(Participant participant);
    }
}
