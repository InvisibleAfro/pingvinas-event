using Microsoft.AspNetCore.Mvc;
using Pingvinas.Event.Core.DTOs;
using Pingvinas.Event.Core.Features.PingvinEvent;

namespace Pingvinas.Event.Api.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class EventController : ControllerBase
{
    private readonly IEventService _service;
    private readonly ILogger<EventController> _logger;

    public EventController(IEventService service, ILogger<EventController> logger)
    {
        _service = service;
        _logger = logger;
    }

    // TODO: Should be able to filter this on events that are still possible to attend.
    [HttpGet]
    public async Task<ActionResult<List<EventDto>>> Get()
        => Ok(await _service.GetEvents());

    [HttpGet("{id}")]
    public async Task<ActionResult<EventDto>> Get(string id)
        => Ok(await _service.GetEvent(id));

    [HttpPost]
    public async Task<ActionResult<string>> CreateEvent([FromBody] EventDto eventDto)
    {
        var e = await _service.CreateEvent(eventDto);
        return Created(string.Empty, new { });
    }

    [HttpPut]
    public async Task<ActionResult<bool>> UpdateEvent([FromBody] EventDto eventDto)
        => Ok(await _service.UpdateEvent(eventDto, true));

    /// <summary>
    /// Cancels the specified event.
    /// </summary>
    /// <param name="eventId"></param>
    /// <returns></returns>
    [HttpDelete("{eventId}")]
    public async Task<ActionResult<bool>> CancelEvent(string eventId)
        => Ok(await _service.CancelEvent(eventId));
}