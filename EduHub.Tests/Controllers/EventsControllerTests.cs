using EduHub.API.Controllers;
using EduHub.Application.Interfaces;
using EduHub.Application.Services;
using EduHub.Domain.Entities;
using EduHub.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace EduHub.Tests.Controllers;

public class EventsControllerTests
{
    private readonly Mock<IEventRepository> _mockRepo;
    private readonly Mock<INotificationService> _mockNotification;
    private readonly EventService _eventService;
    private readonly EventsController _controller;

    public EventsControllerTests()
    {
        _mockRepo = new Mock<IEventRepository>();
        _mockNotification = new Mock<INotificationService>();
        _eventService = new EventService(_mockRepo.Object, _mockNotification.Object);
        _controller = new EventsController(_eventService);
    }

    [Fact]
    public void GetByClubId_ShouldReturnOkWithEventsList()
    {
        // Arrange
        var clubId = Guid.NewGuid();
        var events = new List<Event>
        {
            Event.Create(clubId, "Event 1", DateTime.UtcNow, 50),
            Event.Create(clubId, "Event 2", DateTime.UtcNow, 80)
        };
        _mockRepo.Setup(r => r.GetByClubId(clubId)).Returns(events);

        // Act
        var result = _controller.GetByClubId(clubId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedEvents = Assert.IsAssignableFrom<IEnumerable<Event>>(okResult.Value);
        Assert.Equal(2, returnedEvents.Count());
    }

    [Fact]
    public void GetById_WhenEventExists_ShouldReturnOk()
    {
        // Arrange
        var clubId = Guid.NewGuid();
        var @event = Event.Create(clubId, "AI Workshop", DateTime.UtcNow, 60);
        _mockRepo.Setup(r => r.GetById(@event.Id)).Returns(@event);

        // Act
        var result = _controller.GetById(@event.Id);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedEvent = Assert.IsType<Event>(okResult.Value);
        Assert.Equal(@event.Id, returnedEvent.Id);
    }

    [Fact]
    public void GetById_WhenEventDoesNotExist_ShouldReturnNotFound()
    {
        // Arrange
        var missingId = Guid.NewGuid();
        _mockRepo.Setup(r => r.GetById(missingId)).Returns((Event?)null);

        // Act
        var result = _controller.GetById(missingId);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public void Create_WithValidData_ShouldReturnCreatedAtAction()
    {
        // Arrange
        var clubId = Guid.NewGuid();
        var request = new CreateEventRequest("Hội thảo Khởi nghiệp", DateTime.UtcNow.AddDays(3), 120, "email");

        // Act
        var result = _controller.Create(clubId, request);

        // Assert
        var createdResult = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal(nameof(EventsController.GetById), createdResult.ActionName);
        var createdEvent = Assert.IsType<Event>(createdResult.Value);
        Assert.Equal(request.Title, createdEvent.Title);
        Assert.Equal(request.Capacity, createdEvent.Capacity);
    }

    [Fact]
    public void Create_WithInvalidData_ShouldReturnBadRequest()
    {
        // Arrange: Tiêu đề rỗng hoặc sức chứa <= 0
        var clubId = Guid.NewGuid();
        var request = new CreateEventRequest("", DateTime.UtcNow.AddDays(1), -10, "email");

        // Act
        var result = _controller.Create(clubId, request);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public void Delete_WhenEventExists_ShouldReturnNoContent()
    {
        // Arrange
        var eventId = Guid.NewGuid();
        _mockRepo.Setup(r => r.Delete(eventId)).Returns(true);

        // Act
        var result = _controller.Delete(eventId);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public void Delete_WhenEventDoesNotExist_ShouldReturnNotFound()
    {
        // Arrange
        var eventId = Guid.NewGuid();
        _mockRepo.Setup(r => r.Delete(eventId)).Returns(false);

        // Act
        var result = _controller.Delete(eventId);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }
}
