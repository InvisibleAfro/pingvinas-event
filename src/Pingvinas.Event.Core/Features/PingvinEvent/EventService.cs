using Microsoft.Extensions.Logging;
using Pingvinas.Event.Core.Common;
using Pingvinas.Event.Core.DTOs;
using Pingvinas.Event.Domain.Models;
using Pingvinas.Event.Domain.Repositories;
using System.Data.Common;

namespace Pingvinas.Event.Core.Features.PingvinEvent;

public class EventService(IEventRepository repository, ILogger<EventService> logger, CurrentUser currentUser,
    INotificationService notificationService) : IEventService
{
    private readonly IEventRepository _repository = repository;
    private readonly ILogger<EventService> _logger = logger;
    private readonly CurrentUser _currentUser = currentUser;
    private readonly INotificationService _notificationService = notificationService;

    public async Task<bool> AttendEvent(string eventId)
    {
        try
        {
            var userId = _currentUser.Id;
            await _repository.AddParticipantAsync(new Participant(Guid.NewGuid().ToString(), eventId, userId!));
            return true;
        }
        catch (DbException e)
        {
            _logger.LogError(e, "Registering event attendance failed");
            return false;
        }
    }

    public async Task<bool> CreateEvent(EventDto eventDto)
    {
        try
        {
            await _repository.CreateEvent(MapFromDto(eventDto));
            return true;
        }
        catch (DbException e)
        {
            _logger.LogError(e, "Creating event failed");
            return false;
        }
    }

    public async Task<bool> UpdateEvent(EventDto eventDto, bool notifyParticipants)
    {
        await _repository.UpdateEvent(MapFromDto(eventDto));
        if (notifyParticipants)
        {
            var participants = await _repository.GetEventParticipants(eventDto.Id);
            foreach (var participant in participants)
            {
                await SendEventUpdatedNotification(participant, $"Event '{eventDto.Title}' has been updated. Please check the details.");
            }
        }

        return true;
    }

    public async Task<bool> CancelEvent(string eventId)
    {
        var e = await _repository.GetEvent(eventId);
        foreach (var participant in e.Participants ?? [])
        {
            await SendEventUpdatedNotification(participant, $"Event '{e.Title}' has been cancelled. We apologize for any inconvenience.");
        }

        await _repository.CancelEvent(eventId);
        return true;
    }

    public async Task<EventDto> GetEvent(string eventId)
    {
        return MapFromEntity(await _repository.GetEvent(eventId));
    }

    public async Task<IList<EventDto>> GetEvents()
    {
        var events = await _repository.GetEvents();
        return [.. events.Select(MapFromEntity)];
    }

    private async Task SendEventUpdatedNotification(Participant participant, string message)
    {
        await _notificationService.NotifyParticipantAsync(participant, message);
    }

    private EventDto MapFromEntity(Domain.Models.PingvinEvent pingvinEvent)
    {
        return new EventDto
        {
            Id = pingvinEvent.Id,
            Title = pingvinEvent.Title,
            Description = pingvinEvent.Description,
            OwnerName = pingvinEvent.OwnerName,
            Location = pingvinEvent.Location,
            Summary = pingvinEvent.Summary,
            StartDate = pingvinEvent.StartDate,
            EndDate = pingvinEvent.EndDate,
            ResponseDeadline = pingvinEvent.ResponseDeadline,
            NumberOfGuestsAllowed = pingvinEvent.NumberOfGuestsAllowed,
            RequireResponse = pingvinEvent.RequireResponse,
            MaxParticipants = pingvinEvent.MaxParticipants,
            MinParticipants = pingvinEvent.MinParticipants,
            ParticipantCount = pingvinEvent.ParticipantCount,
            IsSocial = pingvinEvent.IsSocial
        };
    }

    private Domain.Models.PingvinEvent MapFromDto(EventDto eventDto)
    {

        var userId = _currentUser.Id;
        var userName = _currentUser.Name;
        var userEmail = _currentUser.Email;

        var pingvingEvent = new Domain.Models.PingvinEvent
        {
            Id = eventDto.Id,
            Title = eventDto.Title,
            Description = eventDto.Description,
            CreatorId = userId,
            Creator = new User(userId, userName, userEmail, true),
            OwnerName = eventDto.OwnerName,
            Location = eventDto.Location,
            Summary = eventDto.Summary,
            StartDate = eventDto.StartDate,
            EndDate = eventDto.EndDate,
            ResponseDeadline = eventDto.ResponseDeadline,
            NumberOfGuestsAllowed = eventDto.NumberOfGuestsAllowed,
            RequireResponse = eventDto.RequireResponse,
            MaxParticipants = eventDto.MaxParticipants,
            MinParticipants = eventDto.MinParticipants,
            ParticipantCount = eventDto.ParticipantCount,
            IsSocial = eventDto.IsSocial
        };

        return pingvingEvent;
    }
}
