using EduHub.Application.Interfaces;

namespace EduHub.Infrastructure.Services;

/// <summary>
/// Factory khởi tạo NotificationService dựa trên tên kênh thông báo.
/// </summary>
public class NotificationServiceFactory : INotificationServiceFactory
{
    public INotificationService Create(string channel)
    {
        return channel?.Trim().ToLowerInvariant() switch
        {
            "email" => new EmailNotificationService(),
            "sms" => new SmsNotificationService(),
            _ => throw new ArgumentException($"Kênh thông báo không được hỗ trợ: '{channel}'. Hỗ trợ 'email' hoặc 'sms'.", nameof(channel))
        };
    }
}
