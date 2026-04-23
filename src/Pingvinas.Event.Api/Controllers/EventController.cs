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
        if (string.IsNullOrWhiteSpace(id) || !Guid.TryParse(id, out _))
            return BadRequest("Event ID is required.");

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
        //if invalid return BadRequest with validation errors

        var e = await _service.CreateEvent(eventDto);
        if (e == null)
            return Problem("Failed to create event.");

        return CreatedAtAction(nameof(GetById), new { id = e.Id }, e);
    }

    [HttpPut]
    public async Task<ActionResult<bool>> UpdateEvent([FromBody] EventDto eventDto)
    {
        return Ok(await _service.UpdateEvent(eventDto, true));
    }

    [HttpDelete("{eventId}")]
    public async Task<ActionResult<bool>> CancelEvent(string eventId)
    {
        return Ok(await _service.CancelEvent(eventId));
    }
}