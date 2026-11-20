using EduHub.Domain.Enums;

namespace EduHub.Domain.Entities;

/// <summary>
/// Đại diện cho phiếu đăng ký tham dự sự kiện (Registration).
/// </summary>
public class Registration
{
    public Guid Id { get; private set; }
    public Guid MemberId { get; private set; }
    public Guid EventId { get; private set; }
    public DateTime RegisteredAt { get; private set; }
    public RegistrationStatus Status { get; private set; }

    // Constructor private ngăn chặn việc tạo đối tượng tùy tiện bên ngoài
    private Registration() { }

    /// <summary>
    /// Factory method tạo phiếu đăng ký mới với trạng thái mặc định là Pending.
    /// </summary>
    /// <param name="memberId">Mã thành viên.</param>
    /// <param name="eventId">Mã sự kiện.</param>
    /// <returns>Phiếu đăng ký mới.</returns>
    /// <exception cref="ArgumentException">Ném ra khi memberId hoặc eventId là Guid.Empty.</exception>
    public static Registration Create(Guid memberId, Guid eventId)
    {
        if (memberId == Guid.Empty)
        {
            throw new ArgumentException("MemberId cannot be empty.", nameof(memberId));
        }

        if (eventId == Guid.Empty)
        {
            throw new ArgumentException("EventId cannot be empty.", nameof(eventId));
        }

        return new Registration
        {
            Id = Guid.NewGuid(),
            MemberId = memberId,
            EventId = eventId,
            RegisteredAt = DateTime.UtcNow,
            Status = RegistrationStatus.Pending
        };
    }

    /// <summary>
    /// Xác nhận phiếu đăng ký. Chỉ cho phép khi trạng thái hiện tại là Pending.
    /// </summary>
    /// <exception cref="InvalidOperationException">Ném ra khi trạng thái không phải Pending.</exception>
    public void Confirm()
    {
        if (Status != RegistrationStatus.Pending)
        {
            throw new InvalidOperationException($"Cannot confirm registration with status '{Status}'. Only Pending registrations can be confirmed.");
        }

        Status = RegistrationStatus.Confirmed;
    }

    /// <summary>
    /// Huỷ phiếu đăng ký. Cho phép khi trạng thái là Pending hoặc Confirmed.
    /// Phiếu đã huỷ không thể thay đổi được nữa.
    /// </summary>
    /// <exception cref="InvalidOperationException">Ném ra khi phiếu đã ở trạng thái Cancelled.</exception>
    public void Cancel()
    {
        if (Status == RegistrationStatus.Cancelled)
        {
            throw new InvalidOperationException("Registration has already been cancelled and cannot be changed.");
        }

        Status = RegistrationStatus.Cancelled;
    }
}
