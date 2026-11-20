using EduHub.Application.Interfaces;

namespace EduHub.Infrastructure.Services;

/// <summary>
/// Dịch vụ gửi thông báo qua Email.
/// </summary>
public class EmailNotificationService : INotificationService
{
    public void Send(string message)
    {
        Console.WriteLine($"[EMAIL] {message}");
    }
}
