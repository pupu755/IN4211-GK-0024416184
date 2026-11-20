namespace EduHub.Application.Interfaces;

/// <summary>
/// Interface hợp đồng gửi thông báo.
/// </summary>
public interface INotificationService
{
    void Send(string message);
}
