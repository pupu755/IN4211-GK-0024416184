using EduHub.Domain.Entities;
using EduHub.Domain.Enums;
using Xunit;

namespace EduHub.Tests.Entities;

public class RegistrationTests
{
    [Fact]
    public void Create_WithValidParameters_ShouldInitializePendingStatus()
    {
        // Arrange
        var memberId = Guid.NewGuid();
        var eventId = Guid.NewGuid();

        // Act
        var registration = Registration.Create(memberId, eventId);

        // Assert
        Assert.NotEqual(Guid.Empty, registration.Id);
        Assert.Equal(memberId, registration.MemberId);
        Assert.Equal(eventId, registration.EventId);
        Assert.Equal(RegistrationStatus.Pending, registration.Status);
    }

    [Fact]
    public void Create_WithEmptyGuid_ShouldThrowArgumentException()
    {
        // Arrange & Act & Assert
        Assert.Throws<ArgumentException>(() => Registration.Create(Guid.Empty, Guid.NewGuid()));
        Assert.Throws<ArgumentException>(() => Registration.Create(Guid.NewGuid(), Guid.Empty));
    }

    [Fact]
    public void Confirm_WhenPending_ShouldChangeStatusToConfirmed()
    {
        // Arrange
        var registration = Registration.Create(Guid.NewGuid(), Guid.NewGuid());

        // Act
        registration.Confirm();

        // Assert
        Assert.Equal(RegistrationStatus.Confirmed, registration.Status);
    }

    [Fact]
    public void Cancel_WhenPendingOrConfirmed_ShouldChangeStatusToCancelled()
    {
        // Arrange
        var reg1 = Registration.Create(Guid.NewGuid(), Guid.NewGuid());
        var reg2 = Registration.Create(Guid.NewGuid(), Guid.NewGuid());
        reg2.Confirm();

        // Act
        reg1.Cancel();
        reg2.Cancel();

        // Assert
        Assert.Equal(RegistrationStatus.Cancelled, reg1.Status);
        Assert.Equal(RegistrationStatus.Cancelled, reg2.Status);
    }

    /// <summary>
    /// Kiểm tra ca kiểm thử bắt buộc theo yêu cầu đề bài:
    /// Huỷ phiếu rồi xác nhận phải ném InvalidOperationException và Status vẫn giữ nguyên là Cancelled.
    /// </summary>
    [Fact]
    public void Confirm_WhenCancelled_ShouldThrowInvalidOperationExceptionAndRemainCancelled()
    {
        // Arrange
        var memberId = Guid.NewGuid();
        var eventId = Guid.NewGuid();
        var r = Registration.Create(memberId, eventId);
        r.Cancel();

        // Act & Assert
        var ex = Assert.Throws<InvalidOperationException>(() => r.Confirm());
        Assert.Equal(RegistrationStatus.Cancelled, r.Status);
    }
}
