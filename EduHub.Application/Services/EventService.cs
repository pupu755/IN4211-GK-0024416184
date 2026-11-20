using EduHub.Application.Interfaces;
using EduHub.Domain.Entities;
using EduHub.Domain.Interfaces;

namespace EduHub.Application.Services;

/// <summary>
/// Dịch vụ nghiệp vụ quản lý sự kiện.
/// Áp dụng Dependency Injection và tuân thủ các nguyên lý SOLID.
/// </summary>
public class EventService
{
    private readonly IEventRepository _eventRepository;
    private readonly INotificationService? _notificationService;
    private readonly INotificationServiceFactory? _notificationServiceFactory;

    /// <summary>
    /// Khởi tạo EventService với notification service trực tiếp (thích hợp cho unit test hoặc cấu hình đơn kênh).
    /// </summary>
    public EventService(IEventRepository eventRepository, INotificationService notificationService)
    {
        _eventRepository = eventRepository ?? throw new ArgumentNullException(nameof(eventRepository));
        _notificationService = notificationService ?? throw new ArgumentNullException(nameof(notificationService));
    }

    /// <summary>
    /// Khởi tạo EventService với notification factory (hỗ trợ chọn kênh động như email, sms).
    /// </summary>
    public EventService(IEventRepository eventRepository, INotificationServiceFactory notificationServiceFactory)
    {
        _eventRepository = eventRepository ?? throw new ArgumentNullException(nameof(eventRepository));
        _notificationServiceFactory = notificationServiceFactory ?? throw new ArgumentNullException(nameof(notificationServiceFactory));
    }

    /// <summary>
    /// Tạo sự kiện mới, lưu vào repository và gửi thông báo.
    /// </summary>
    public Event CreateEvent(Guid clubId, string title, DateTime date, int capacity, string channel = "email")
    {
        var @event = Event.Create(clubId, title, date, capacity);
        _eventRepository.Add(@event);

        var notifier = _notificationService ?? _notificationServiceFactory?.Create(channel);
        notifier?.Send($"Sự kiện mới: {title}");

        return @event;
    }

    public IEnumerable<Event> GetEventsByClubId(Guid clubId)
    {
        return _eventRepository.GetByClubId(clubId);
    }

    public Event? GetEventById(Guid id)
    {
        return _eventRepository.GetById(id);
    }

    public bool DeleteEvent(Guid id)
    {
        return _eventRepository.Delete(id);
    }
}
