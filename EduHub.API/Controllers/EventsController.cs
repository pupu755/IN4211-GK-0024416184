using EduHub.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace EduHub.API.Controllers;

[ApiController]
[Route("api")]
public class EventsController : ControllerBase
{
    private readonly EventService _eventService;

    public EventsController(EventService eventService)
    {
        _eventService = eventService;
    }

    /// <summary>
    /// Lấy danh sách sự kiện của một câu lạc bộ.
    /// </summary>
    /// <param name="clubId">Mã định danh câu lạc bộ.</param>
    /// <returns>Danh sách sự kiện (200 OK).</returns>
    [HttpGet("clubs/{clubId:guid}/events")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetByClubId(Guid clubId)
    {
        var events = _eventService.GetEventsByClubId(clubId);
        return Ok(events);
    }

    /// <summary>
    /// Xem thông tin chi tiết một sự kiện theo Id.
    /// </summary>
    /// <param name="id">Mã sự kiện.</param>
    /// <returns>200 OK nếu tìm thấy, 404 Not Found nếu không tồn tại.</returns>
    [HttpGet("events/{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetById(Guid id)
    {
        var @event = _eventService.GetEventById(id);
        if (@event is null)
        {
            return NotFound(new { message = $"Sự kiện với mã '{id}' không tồn tại." });
        }

        return Ok(@event);
    }

    /// <summary>
    /// Tạo một sự kiện mới cho câu lạc bộ.
    /// </summary>
    /// <param name="clubId">Mã câu lạc bộ tổ chức.</param>
    /// <param name="request">Thông tin sự kiện cần tạo.</param>
    /// <returns>201 Created nếu tạo thành công, 400 Bad Request nếu dữ liệu không hợp lệ.</returns>
    [HttpPost("clubs/{clubId:guid}/events")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Create(Guid clubId, [FromBody] CreateEventRequest request)
    {
        if (request is null)
        {
            return BadRequest(new { message = "Dữ liệu yêu cầu không được rỗng." });
        }

        try
        {
            var @event = _eventService.CreateEvent(
                clubId,
                request.Title,
                request.Date,
                request.Capacity,
                request.Channel ?? "email"
            );

            return CreatedAtAction(nameof(GetById), new { id = @event.Id }, @event);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Xoá một sự kiện theo Id.
    /// </summary>
    /// <param name="id">Mã sự kiện cần xoá.</param>
    /// <returns>204 NoContent nếu xoá thành công, 404 Not Found nếu không tìm thấy.</returns>
    [HttpDelete("events/{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Delete(Guid id)
    {
        var deleted = _eventService.DeleteEvent(id);
        if (!deleted)
        {
            return NotFound(new { message = $"Sự kiện với mã '{id}' không tồn tại để xoá." });
        }

        return NoContent();
    }
}

/// <summary>
/// DTO tiếp nhận dữ liệu tạo mới sự kiện.
/// </summary>
public record CreateEventRequest(string Title, DateTime Date, int Capacity, string? Channel = "email");
