using Pingvinas.Event.Domain.Models;

namespace Pingvinas.Event.Domain.Repositories;

public interface IEventRepository
{
    Task AddParticipantAsync(Participant participant);
    Task RemoveParticipantAsync(Participant participant);
    Task<PingvinEvent> CreateEvent(PingvinEvent @event);
    Task<PingvinEvent> UpdateEvent(PingvinEvent @event);
    Task<PingvinEvent> GetEvent(string eventId);
    Task<IEnumerable<PingvinEvent>> GetEvents();
    Task<List<Participant>> GetEventParticipants(string eventId);
    Task CancelEvent(string eventId);
}