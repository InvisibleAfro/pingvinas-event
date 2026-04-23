using Pingvinas.Event.Core.DTOs;

namespace Pingvinas.Event.Core.Features.PingvinEvent;

public interface IEventService
{
    /// <summary>
    /// A user can register him-/her- self as attending an event.
    /// The userId is collected from claims. 
    /// </summary>
    /// <param name="eventId">The event the user wants to attend.</param>
    /// <returns>The value "true" indicating success or "false" indicating failure.</returns>
    Task<bool> AttendEvent(string eventId);

    /// <summary>
    /// Users can create new events.
    /// </summary>
    /// <param name="event">The details of the event that will be created.</param>
    /// <returns>A success-flag.</returns>
    Task<EventDto> CreateEvent(EventDto eventDto);

    /// <summary>
    /// Allows a user to make changes to an event.
    /// Users can only update their own events.
    /// </summary>
    /// <param name="event">The updated event.</param>
    /// <param name="notifyParticipants">Lets the user notify the participants of an event of the changes.</param>
    /// <returns></returns>
    Task<bool> UpdateEvent(EventDto @event, bool notifyParticipants);

    /// <summary>
    /// Marks the event as cancelled and notifies participants of the cancellation.
    /// Users can only cancel their own events.
    /// </summary>
    /// <param name="eventId">The id of the event that will be cancelled.</param>
    /// <returns></returns>
    Task<bool> CancelEvent(string eventId);

    /// <summary>
    /// Gets a single event with as many details as are available based on the eventId.
    /// </summary>
    /// <param name="eventId">The id of the requested event.</param>
    /// <returns></returns>
    Task<EventDto?> GetEvent(string eventId);

    /// <summary>
    /// Gets all events in the database.
    /// </summary>
    /// <returns></returns>
    Task<IList<EventDto>> GetEvents();
}
