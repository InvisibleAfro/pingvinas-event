using Pingvinas.Event.Domain.Models;

namespace Pingvinas.Event.Core.Features
{
    public interface INotificationService
    {
        Task NotifyParticipantAsync(Participant participant, string message);
    }
}
