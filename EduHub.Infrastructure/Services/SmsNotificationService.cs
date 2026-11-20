using EduHub.Application.Interfaces;

namespace EduHub.Infrastructure.Services;

/// <summary>
/// Dịch vụ gửi thông báo qua SMS.
/// </summary>
public class SmsNotificationService : INotificationService
{
    public void Send(string message)
    {
        Console.WriteLine($"[SMS] {message}");
    }
}
