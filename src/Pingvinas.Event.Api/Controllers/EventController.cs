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
    [ProducesResponseType(typeof(List<EventDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<EventDto>>> Get()
    {
        return Ok(await _service.GetEvents());
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(EventDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<EventDto>> GetById(string id)
    {
        if (!IsValidId(id))
            return BadRequest("A valid event ID is required.");

        var e = await _service.GetEvent(id);

        if (e == null)
            return NotFound();

        return Ok(e);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<EventDto>> CreateEvent([FromBody] EventDto eventDto)
    {
        var createdEvent = await _service.CreateEvent(eventDto);
        return CreatedAtAction(nameof(GetById), new { id = createdEvent.Id }, createdEvent);
    }

    [HttpPut]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<bool>> UpdateEvent([FromBody] EventDto eventDto)
    {
        if (!IsValidId(eventDto.Id))
            return BadRequest("A valid event ID is required.");

        if (await _service.GetEvent(eventDto.Id) is null)
            return NotFound();

        return Ok(await _service.UpdateEvent(eventDto, true));
    }

    [HttpDelete("{eventId}")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<bool>> CancelEvent(string eventId)
    {
        if (!IsValidId(eventId))
            return BadRequest("A valid event ID is required.");

        if (await _service.GetEvent(eventId) is null)
            return NotFound();

        return Ok(await _service.CancelEvent(eventId));
    }

    private static bool IsValidId(string id) => !string.IsNullOrWhiteSpace(id) && Guid.TryParse(id, out _);
}
