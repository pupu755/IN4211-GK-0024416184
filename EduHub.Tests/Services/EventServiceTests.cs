using EduHub.Application.Interfaces;
using EduHub.Application.Services;
using EduHub.Domain.Entities;
using EduHub.Domain.Interfaces;
using Moq;
using Xunit;

namespace EduHub.Tests.Services;

public class EventServiceTests
{
    [Fact]
    public void CreateEvent_WithValidData_ShouldCallAddAndSendExactlyOnce()
    {
        // Arrange
        var mockRepo = new Mock<IEventRepository>();
        var mockNotification = new Mock<INotificationService>();

        var eventService = new EventService(mockRepo.Object, mockNotification.Object);

        var clubId = Guid.NewGuid();
        var title = "Hội thảo Công nghệ Phần mềm";
        var date = DateTime.UtcNow.AddDays(7);
        var capacity = 100;

        // Act
        var result = eventService.CreateEvent(clubId, title, date, capacity, "email");

        // Assert
        Assert.NotNull(result);
        Assert.Equal(title, result.Title);
        Assert.Equal(capacity, result.Capacity);

        // Kiểm tra Add của IEventRepository được gọi đúng một lần
        mockRepo.Verify(r => r.Add(It.Is<Event>(e => e.Title == title && e.Capacity == capacity)), Times.Once);

        // Kiểm tra Send của INotificationService được gọi đúng một lần
        mockNotification.Verify(n => n.Send(It.Is<string>(msg => msg.Contains(title))), Times.Once);
    }
}
